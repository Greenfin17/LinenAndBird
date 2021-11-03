const firebaseConfig = {
  apiKey: "AIzaSyCUOfqjrQMLAn4P59JCr2hsSrhIknBrZCA",
  authDomain: "sports-roster-42025.firebaseapp.com",
  databaseURL: "https://sports-roster-42025-default-rtdb.firebaseio.com",
  projectId: "sports-roster-42025",
  storageBucket: "sports-roster-42025.appspot.com",
  messagingSenderId: "761885807622",
  appId: "1:761885807622:web:19e4d500a96f8da1cb9f87",
  measurementId: "G-B01MRFY7EJ"
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);
const analytics = getAnalytics(app);
