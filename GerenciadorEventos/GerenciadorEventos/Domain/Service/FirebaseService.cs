using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorEventos.Domain.Service
{
    public static class FirebaseService
    {
        private static readonly string _linkFirebase = "https://labengxamarin-default-rtdb.firebaseio.com/";

        private static FirebaseClient _firebaseClient;

        private static void Initialize()
        {
            if (_firebaseClient == null)
            {
                _firebaseClient = new FirebaseClient(_linkFirebase);
            }
        }
        public static FirebaseClient getConnection()
        {
            Initialize();
            return _firebaseClient;
        }
    }
}