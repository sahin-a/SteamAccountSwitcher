import React from 'react';
import logo from './logo.svg';
import './App.css';
import {AccountItem} from "./steam/presentation/components/switcher/AccountItem";

function App() {
    const accountName = "sahin"
    return (
        <div className="App">
            <AccountItem
                accountName={accountName}
                username="Epic Sahin"
                isBanned={true}
                level={BigInt(100)}
                lastLoginStatus={"last seen 8 hours ago"}
                onAvatarClick={ () => console.log(`avatar clicked ${accountName}`) }
                onAccountClick={ () => console.log(`account clicked ${accountName}`) }
            />
        </div>
    );
}

export default App;
