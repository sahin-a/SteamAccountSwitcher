import axios from "axios";

export class Account {
    readonly name: string;
    readonly username: string;
    readonly isBanned: boolean;
    readonly level: number;
    readonly avatarUrl: string;
    readonly profileUrl: string;

    constructor(name: string, username: string, isBanned: boolean, level: number, avatarUrl: string, profileUrl: string) {
        this.name = name;
        this.username = username;
        this.isBanned = isBanned;
        this.level = level;
        this.avatarUrl = avatarUrl;
        this.profileUrl = profileUrl;
    }
}

export async function switchAccount(name: string) {
    await axios.post("https://192.168.2.250:7027/api/Switcher", {
        accountName: name
    })
}

export async function retrieveAccounts(): Promise<Account[]> {
    const response = await axios.get("https://192.168.2.250:7027/api/Account")

    if (response.status !== 200)
        return []

    const result = response.data.map((a: any) =>
        new Account(a.name, a.username, a.isVacBanned, a.level, a.avatarUrl, a.profileUrl)
    )

    console.log(result)

    return result
}