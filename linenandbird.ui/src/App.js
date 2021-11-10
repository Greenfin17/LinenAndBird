// import logo from './logo.svg';
import { useEffect, useState } from 'react';
import firebase from 'firebase/compat/app';
import 'firebase/compat/auth';
import 'firebase/compat/firestore';
// import { getAuth, onAuthStateChanged } from 'firebase/auth';
import './App.css';
import BirdList from './components/bird/BirdList';
import { getAllBirds } from './helpers/data/birdData';
import { signInUser } from './helpers/auth';

function App() {

  const [birds, setBirds] = useState([]);
  const [user, setUser] = useState({});

  useEffect(() => {
    firebase.auth().onAuthStateChanged((userObj) => {
      if (userObj) {             
        
        //store the token for later   
        userObj.getIdToken().then((token) => sessionStorage.setItem("token", token));
        
        setUser(userObj);
      } else {
        setUser(false);
      }
      console.warn(user);
    });
  }, );  

  useEffect(() => 
    {
      getAllBirds().then(data => setBirds(data))
    },[]);

  return (
    <div className="App">
      <button className='signin-button google-logo' onClick={signInUser} />
      <i className='fas fa-sign-out-alt'>Sign In</i>
      <BirdList birds={birds}/>
    </div>
  );
}

export default App;
