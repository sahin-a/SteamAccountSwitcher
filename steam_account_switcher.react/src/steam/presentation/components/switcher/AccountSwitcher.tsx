import * as React from 'react';
import {Account, retrieveAccounts, switchAccount} from "../../../switching/AccountSwitcherService";
import {Stack} from "@mui/material";
import {AccountItem} from "./AccountItem";

interface IProps {
}

interface IState {
    accounts?: Account[]
}

export class AccountSwitcher extends React.Component<IProps, IState> {

    constructor(props: IProps) {
        super(props);
        this.state = {accounts: undefined}
    }

    componentDidMount() {
        retrieveAccounts().then(accounts =>
            this.setState({accounts})
        ).catch(() => this.setState({accounts: undefined}))
    }

    render() {
        return <>
            <Stack sx={{padding: 2}} spacing={2}>
                {
                    this.state.accounts?.map((account) => {
                        return <AccountItem
                            accountName={account.name}
                            username={account.username}
                            isBanned={account.isBanned}
                            avatarUrl={account.avatarUrl}
                            level={account.level}
                            lastLoginStatus={"last seen 8 hours ago"}
                            profileUrl={account.profileUrl}
                            onAccountClick={() => switchAccount(account.name)}
                        />
                    })
                }
            </Stack>
        </>
    }
}