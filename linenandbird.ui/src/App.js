// import logo from './logo.svg';
import { useEffect, useState } from 'react';
import { getAuth, onAuthStateChanged } from 'firebase/auth';
import './App.css';
import BirdList from './components/bird/BirdList';
import { getAllBirds } from './helpers/data/birdData';
import { signInUser } from './helpers/auth';

function App() {

  const [birds, setBirds] = useState([]);
  const [user, setUser] = useState({});

  useEffect(() => {
  const auth = getAuth();
  onAuthStateChanged(auth, (userObj) => {
    console.warn(userObj);
    if (user) {
      // store JWT token for later
      user.getIdToken().then((token) => sessionStorage('token', token));
      setUser(user);
    } else {
      setUser(false);
    }
    })
  });

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
