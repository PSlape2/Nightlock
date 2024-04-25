using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace PlayerStuff {
    public class PlayerStats : MonoBehaviour
    {
        public static int health;

        void Start()
        {
            setHealth(20);
        }
        public static void healthUpdate(int changeAmount) {
            health += changeAmount;
            Debug.Log(health);
        }
        public static void setHealth(int setAmount) {
            health = setAmount;
        }
        void drawHealth() {
            
        }
    }
}
