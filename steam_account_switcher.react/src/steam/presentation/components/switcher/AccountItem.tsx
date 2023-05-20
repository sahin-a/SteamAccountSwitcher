import Avatar from '@mui/material/Avatar';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import {CardMedia, Grid, Stack} from "@mui/material";

interface IProps {
    accountName: string,
    username: string,
    level: number,
    lastLoginStatus: string,
    isBanned: Boolean,
    avatarUrl: string,
    profileUrl: string,
    onAccountClick: () => void
}

export const AccountItem = (
    {
        accountName,
        username,
        level,
        lastLoginStatus,
        isBanned,
        avatarUrl,
        profileUrl,
        onAccountClick,
    }: IProps
) => {

    return (
        <Card onClick={onAccountClick}>
            <CardMedia
                sx={{height: 128, objectFit: 'scale-down'}}
                image={avatarUrl ? (avatarUrl) : ("https://media.istockphoto.com/vectors/default-avatar-photo-placeholder-icon-grey-profile-picture-business-vector-id1327592449?k=20&m=1327592449&s=612x612&w=0&h=6yFQPGaxmMLgoEKibnVSRIEnnBgelAeIAf8FqpLBNww=")}>
            </CardMedia>
            <CardContent sx={{
                "&:last-child": {
                    paddingBottom: 2
                }
            }}>
                <Grid container spacing={0} sx={{alignItems: "center"}}>
                    {level >= 0 ? <Grid item>
                        <Avatar sx={{fontSize: 16, verticalAlign: "center"}}>{level.toString()}</Avatar>
                    </Grid> : null
                    }

                    {isBanned ? <Grid item sx={{paddingLeft: 2, fontSize: 26}}>🛡</Grid> : null}

                    <Stack sx={{paddingLeft: 2}}>
                        <a>{!username ? accountName : "••••"}</a>
                        <a>{username}</a>
                    </Stack>
                </Grid>
            </CardContent>
        </Card>
    )
}