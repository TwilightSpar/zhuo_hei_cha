import React from 'react';
import './App.css';
import Home from './Components/Home';
import { Switch, Route } from 'react-router-dom';
import GameRoom from './Components/GameRoom';
import ErrorPage from './Components/ErrorPage';
import 'bootstrap/dist/css/bootstrap.min.css';

function App() {
  return (
    <Switch>
      <Route path='/' component={Home} exact />
      <Route path='/game' component={GameRoom} />
      <Route component={ErrorPage} />
    </Switch>
  );
}

export default App;
