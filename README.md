# Bot for Time Entry
This little program has the purpose of recording the time entry for Qual.

## Opportunities for improvement
This app has been made with the minimum necessary to record the clock-in and clock-out. However, improvements can be made and next I list some new features that could be incorporated:

- Handle the case where the user is going to record their first time entry.
- Notify to the user (mail, messenger app, etc.) when a new entry can't be recorded on a valid time frame.
- Handle the case when the app detects that some previous entries were not recorded in their appropiate time frame.

## Notes
This app doesn't connect to the Qual VPN necessary to reach the server. Research was done about how to make the program connect to the VPN, however, all intents were in vain. Supposedly, FortiClient VPN uses SSL-VPN which is a propietary VPN and it is not an open VPN. Given this, it is apparently needed an API to connect to the VPN server or use a CLI command on a utility program for FortiClient. I searched for this but got no luck :(

But don't discourage on finding a way to make the c# app able to connect to the VPN server, making the app independent of having an open VPN connection on the host computer. 
