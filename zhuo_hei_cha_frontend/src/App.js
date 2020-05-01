import React from 'react';
import logo from './logo.svg';
import { HubConnectionBuilder } from '@aspnet/signalr';
import './App.css';

const sendRequest = () => {
  fetch('http://localhost:5000/WeatherForecast')
    .then((response) => response.json())
    .then(data => console.log(data))
};

const s = async () => {
  const c = new HubConnectionBuilder().withUrl('http://localhost:5000/samplehub').build();
  await c.start();
  c.invoke('SendMessage').then(s => console.log(s)).catch(err => console.error(err));
  // c.on('ReceiveMessage', () => {})

  // c.on('a', () => {
  //   alert('bla')
  //   c.invoke('AnswerBlackAce', false);
  // })


  const AskPlayOneMoreRound = async () => {
    // do stuff
    await c.invoke('Return')
  }
  

}

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.js</code> and save to reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
        <button onClick={s}></button>
      </header>
    </div>
  );
}



export default App;
