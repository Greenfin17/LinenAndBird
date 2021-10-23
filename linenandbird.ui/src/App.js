// import logo from './logo.svg';
import {useEffect, useState} from 'react';
import './App.css';
import BirdList from './components/bird/BirdList';
import { getAllBirds } from './helpers/data/birdData';

function App() {

  const [birds, setBirds] = useState([]);
  useEffect(() => 
    {
      getAllBirds().then(data => setBirds(data))
    },[]);

  return (
    <div className="App">
      <BirdList birds={birds}/>
    </div>
  );
}

export default App;
