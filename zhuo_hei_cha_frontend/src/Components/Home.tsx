import React from 'react';
import SampleComponent from './SampleComponent';
import logo from '../logo.svg';

type IProps = {};

const Home: React.FunctionComponent<IProps> = () => {
    return (
        <div className="App">
            <header className="App-header">
                <img src={logo} className="App-logo" alt="logo" />
                <p>
                Edit <code>src/App.tsx</code> and save to reload.
                </p>
                <a
                className="App-link"
                href="https://reactjs.org"
                target="_blank"
                rel="noopener noreferrer"
                >
                Learn React
                </a>
                <SampleComponent />
            </header>
        </div>
    )
}

export default Home;