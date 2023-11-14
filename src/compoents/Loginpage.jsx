import React, { useState } from 'react';
import {useNavigate } from 'react-router-dom'; // Import useNavigate
import axios from 'axios';
import './Styles/login.css';

function Loginpage() {
  const [Email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [errorMessage, setErrorMessage] = useState('');
  const navigate = useNavigate();

  const handleUsernameChange = (e) => {
    setEmail(e.target.value);
  };

  const handlePasswordChange = (e) => {
    setPassword(e.target.value);
  };

  const handleLogin = async () => {
    try {
      const response = await axios.post('https://localhost:7101/api/User/verify', {
        Email: Email,
        Password: password,
      });

      if (response.data === 'Wrong Password' || response.data === 'Not Found') {
        setErrorMessage('Verification unsuccessful');
        console.log("verification failed")
      } else {
        navigate('/dashboardpage');
        console.log("verification success")
      }
    } catch (error) {
      console.error('Error:', error);
      setErrorMessage('Verification Unsuccessful');
    }
  };

  return (
    <div className="login">
      <div className="login-container">
        <div className="header">
          <div className="text">Sign In</div>
        </div>
        <div className="inputs">
          <div className="input">
            <input
              className="username"
              type="text"
              value={Email}
              placeholder="EMAIL"
              onChange={handleUsernameChange}
            />
          </div>
          <div className="input">
            <input
              className="password"
              type="password"
              value={password}
              placeholder="PASSWORD"
              onChange={handlePasswordChange}
            />
          </div>
        </div>
        <div className="submit-container">
          <div className="submit">
            <button className='Login-btn' onClick={handleLogin}>Login</button>
          </div>
        </div>
        <div className="line"></div>
        {errorMessage && <div className="error-message">{errorMessage}</div>}
      </div>
    </div>
  );
}

export default Loginpage;
