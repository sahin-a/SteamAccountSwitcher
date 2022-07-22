import Avatar from '@mui/material/Avatar';
import Typography from '@mui/material/Typography';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import {Grid, Stack} from "@mui/material";

interface IProps {
    accountName: String,
    username: String,
    level: BigInt,
    lastLoginStatus: String,
    isBanned: Boolean,
    onAvatarClick: () => void,
    onAccountClick: () => void
}

export const AccountItem = ({accountName, username, level, lastLoginStatus, isBanned, onAvatarClick, onAccountClick}: IProps) => {

    return (
        <Card onClick={onAccountClick}>
            <CardContent>
                <Grid container sx={{alignItems: "center"}}>
                    <Grid item>
                            <Avatar
                                onClick={onAvatarClick}
                                src="https://media.istockphoto.com/vectors/default-avatar-photo-placeholder-icon-grey-profile-picture-business-vector-id1327592449?k=20&m=1327592449&s=612x612&w=0&h=6yFQPGaxmMLgoEKibnVSRIEnnBgelAeIAf8FqpLBNww="
                            />
                    </Grid>
                    {isBanned ? <Grid sx={{paddingLeft: 2, paddingRight: 2, fontSize: 26}} item>🛡</Grid> : null}
                    <Grid item>
                        <Typography align={"left"}>
                            <header>{accountName}</header>
                            <body>{username}</body>
                        </Typography>
                    </Grid>
                    <Grid item xs={5}>{lastLoginStatus}</Grid>
                    <Grid item>
                        <Avatar sx={{fontSize: 16, verticalAlign: "center"}}>{level.toString()}</Avatar>
                    </Grid>
                </Grid>
            </CardContent>
        </Card>
    )
}