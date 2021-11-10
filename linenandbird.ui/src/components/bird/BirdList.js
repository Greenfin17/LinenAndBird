import React from "react";
import BirdCard from "./BirdCard";

export default function BirdList({birds}) {
  let birdCards = birds.map((bird, index) =><BirdCard 
    key={index}
    bird={bird}/>); 
  return (
  <div>
    {birdCards}
  </div>
  );
}
