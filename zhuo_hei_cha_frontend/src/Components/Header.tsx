import React, { CSSProperties } from 'react';
import { Navbar } from 'react-bootstrap';

const Header: React.FunctionComponent<{}> = () => {
    return (
    <Navbar style={style}>
        <Navbar.Brand href="#home">ZHC</Navbar.Brand>
        <Navbar.Toggle />
        <Navbar.Collapse className="justify-content-end">
        <Navbar.Text>
            Welcome, TestUser!
        </Navbar.Text>
        </Navbar.Collapse>
    </Navbar>
    )
}

const style: CSSProperties = {
    boxShadow: '3px 3px 3px #a6aeba',
    paddingLeft: 40,
    paddingRight: 20,
    marginBottom: 10
}

export default Header;
