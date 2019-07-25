import React from 'react';
import './App.css';

function handleClick(e) {
  const socket = new WebSocket('ws://localhost:10000');

  // Connection opened
  socket.addEventListener('open', function (event) {
    socket.send('Hello Server!');
  });

  // Listen for messages
  socket.addEventListener('message', function (event) {
    console.log('Message from server ', event.data);
  });
}


function App() {
  return (
    <div className="App">
      <header className="App-header">
        <input type="button" onClick={handleClick} value="button"/>
      </header>
    </div>
  );
}

export default App;
