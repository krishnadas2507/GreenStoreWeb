import React from 'react';
import './Styles/header.css'


function Header() {

  return (
    <div className='header-section'>
        <div className='title'>GREEN STORE</div>
        <div className='logout'><button className='lt-btn'>Logout</button></div>
    </div>
  );
}

export default Header;
