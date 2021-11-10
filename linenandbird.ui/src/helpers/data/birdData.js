import axios from 'axios';
import lbConfig from './config';
//const db_URL = "https://localhost:44336";
const db_URL = lbConfig.baseUrl;

const getAllBirds = () => new Promise((resolve, reject) =>{
  axios.get(`${db_URL}/api/birds`)
    .then(response => resolve(response.data))
    .catch((error) => reject(error));
});

export {getAllBirds};


// const getAllBirds =  axios.get('${baseUrl}/api/birds');
