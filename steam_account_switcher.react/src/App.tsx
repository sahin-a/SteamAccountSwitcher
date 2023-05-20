import React from 'react';
import './App.css';
import {Container, CssBaseline} from "@mui/material";
import {AccountSwitcher} from "./steam/presentation/components/switcher/AccountSwitcher";
import {createTheme, ThemeProvider} from '@mui/material/styles';

const darkTheme = createTheme({
    palette: {
        mode: 'dark',
    },
});

function App() {
    return <ThemeProvider theme={darkTheme}>
        <CssBaseline/>

        <Container>
            <AccountSwitcher/>
        </Container>
    </ThemeProvider>;
}

export default App;
