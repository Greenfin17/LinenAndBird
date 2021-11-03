// import logo from './logo.svg';
import {useEffect, useState} from 'react';
import firebase from 'firebase/compat/app';
import './App.css';
import BirdList from './components/bird/BirdList';
import { getAllBirds } from './helpers/data/birdData';
import { signInUser } from '../src/helpers/data/auth';

function App() {

  const [birds, setBirds] = useState([]);
  const [user, setUser] = useState({});

  useEffect(() => {
  firebase.auth().onAuthStateChanged((userObj => {
    console.warn(user);
    if (userObj) {
      // store JWT token for later
      user.getIdToken().then((token) => sessionStorage('token', token));
      setUser(userObj);
    } else {
      setUser(false);
    }
    }));
  }, []);

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
