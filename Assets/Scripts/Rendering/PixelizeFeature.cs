using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PixelizeFeature : ScriptableRendererFeature
{
    [Serializable]
    public class CustomPassSettings
    {
        public RenderPassEvent renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
        public int screenHeight = 270;
    }

    [SerializeField] private CustomPassSettings _settings;
    private PixelizePass _customPass;

    public override void Create()
    {
        _customPass = new PixelizePass(_settings);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(_customPass);
    }
}

public class PixelizePass : ScriptableRenderPass
{
    private PixelizeFeature.CustomPassSettings _settings;

    private RenderTargetIdentifier _colorBuffer, _pixelBuffer;
    private int _pixelBufferID = Shader.PropertyToID("_PixelBuffer");

    private Material _material;
    private int _pixelScreenHeight, _pixelScreenWidth;

    public PixelizePass(PixelizeFeature.CustomPassSettings settings)
    {
        this._settings = settings;
        this.renderPassEvent = settings.renderPassEvent;
        if (_material == null) _material = CoreUtils.CreateEngineMaterial("Hidden/Pixelize");
    }

    public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
    {
        _colorBuffer = renderingData.cameraData.renderer.cameraColorTarget;
        RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;

        _pixelScreenHeight = _settings.screenHeight;
        _pixelScreenWidth = (int)(Mathf.Ceil(_pixelScreenHeight * renderingData.cameraData.camera.aspect));

        cmd.SetGlobalFloat("_PixelWidth", _pixelScreenWidth);
        cmd.SetGlobalFloat("_PixelHeight", _pixelScreenHeight);

        _material.SetVector("_BlockCount", new Vector2(_pixelScreenWidth, _pixelScreenHeight));
        _material.SetVector("_BlockSize", new Vector2(1.0f / _pixelScreenWidth, 1.0f / _pixelScreenHeight));
        _material.SetVector("_HalfBlockSize", new Vector2(.5f / _pixelScreenWidth, .5f / _pixelScreenHeight));

        descriptor.height = _pixelScreenHeight;
        descriptor.width = _pixelScreenWidth;

        cmd.GetTemporaryRT(_pixelBufferID, descriptor, FilterMode.Point);
        _pixelBuffer = new RenderTargetIdentifier(_pixelBufferID);
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        CommandBuffer cmd = CommandBufferPool.Get();

        using (new ProfilingScope(cmd, new ProfilingSampler("Pixelize Pass")))
        {
            Blit(cmd, _colorBuffer, _pixelBuffer, _material);
            Blit(cmd, _pixelBuffer, _colorBuffer);
        }

        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }

    public override void OnCameraCleanup(CommandBuffer cmd)
    {
        if (cmd == null) throw new System.ArgumentNullException("cmd");
        cmd.ReleaseTemporaryRT(_pixelBufferID);
    }
}