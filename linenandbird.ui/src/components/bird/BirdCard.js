import React from "react";
import PropTypes from 'prop-types';

export default function BirdCard({bird}){
  return (
    <div>
      Type: {bird.type} <br/>
      Color: {bird.color} <br/>
      Name: {bird.name}<br/>
      </div>
  )
}
BirdCard.propTypes = {
  bird: PropTypes.object
};
