using UnityEngine;
using System.Collections;

namespace SDK {
    public class User {
        private static User s_instance;
        public static User instance() {
            if (s_instance == null) {
                s_instance = new User();
            }
            return s_instance;
        }

        public User() {
        }

        public void showQQ() {
        }
    }
}
