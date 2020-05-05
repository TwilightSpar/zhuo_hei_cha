import React, { Fragment } from 'react';
import Header from './Header';
import GameRoom from './GameRoom';

type IProps = {};

const Home: React.FunctionComponent<IProps> = () => {
    return (
        <Fragment>
            <Header />
            <GameRoom />
        </Fragment>
    )
}

export default Home;