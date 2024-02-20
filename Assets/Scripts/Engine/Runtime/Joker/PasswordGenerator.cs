using UkensJoker.DataArchitecture;
using UnityEngine;

namespace UkensJoker.Engine
{
    public class PasswordGenerator : MonoBehaviour
    {
        [SerializeField] private StringVariable _password;

        private void Start()
        {
            SetNewPassword();
        }

        public void SetNewPassword()
        {
            string password = "";
            int sixCount = 0;
            int nextNumber = Mathf.FloorToInt(Random.Range(0f, 9.99f));
            for (int i = 0; i < 12; i++)
            {
                if (Random.Range(0f, 1f) > 0.5f && nextNumber != 0 && nextNumber != 6)
                    nextNumber = Mathf.FloorToInt(Random.Range(0f, 9.99f));

                if (sixCount == 3)
                {
                    sixCount = 0;
                    nextNumber = Mathf.FloorToInt(Random.Range(0f, 5.99f));
                }

                password += nextNumber.ToString();

                if (nextNumber == 6)
                    sixCount++;
            }

            _password.Value = password;
        }
    }
}
