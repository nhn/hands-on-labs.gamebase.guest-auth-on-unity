# Gamebase Authentication Payco Adapter for Android

## Dependencies

| Module | Dependencies | Description |
| ------- | ------------ | ----------- |
| gamebase-adapter-auth-payco  | gamebase-sdk-base-{version}.aar | Gamebase Android SDK |
|                              | paycologin-{version}.aar | Payco Login SDK |
|                              | gson-2.2.4.aar<br>play-services-basement-{version}.aar | paycologin dependencies |

## Release Note

| Date | Payco Adapter Version | Gamebase SDK Version | Payco Login SDK Version | Description |
| ---- | --------------------- | -------------------- | ----------------------- | ----------- |
| 2017.03.21 | 1.1.1   | 1.1.0 ~ 1.1.2   | 1.2.8 ~ 1.3.2 | - Initial version |
| 2017.04.20 | 1.1.3   | 1.1.3 ~ 1.1.3.2 | 1.2.8 ~ 1.3.2 | - Change the Interface |
| 2017.04.28 | 1.1.3.2 | 1.1.3 ~ 1.1.3.2 | 1.2.8 ~ 1.3.2 | - Update Payco SDK to 1.2.9 |
| 2017.05.02 | 1.1.3.3 | 1.1.3.3         | 1.2.8 ~ 1.3.2 | - In the Gamebase sandbox environment, set the payco's operating environment to DEMO. |
| 2017.05.23 | 1.1.4   | 1.1.4 ~ 1.5.0   | 1.2.8 ~ 1.3.2 | - Update version |
| 2017.06.30 | 1.1.4.2 | 1.1.4.2 ~ 1.5.0 | 1.2.8 ~ 1.3.2 | - If it is a SANDBOX environment or the Gamebase service zone is not REAL, it will try to authenticate in Payco's DEMO environment. |
| 2017.07.19 | 1.1.5   | 1.1.5 ~ 1.5.0   | 1.2.8 ~ 1.3.2 | - Update version |
| 2017.09.15 | 1.2.0   | 1.1.5 ~ 1.5.0   | 1.2.8 ~ 1.3.2 | - Change zone type by TOAST Cloud project zone. Now BETA project will authenticate to the REAL NEOID. |
| 2018.02.22 | 1.7.0   | 1.7.0 ~         | 1.2.8 ~ 1.3.2 | - Change the logic that refers from the console setting value.<br>- Update Payso SDK to 1.3.2 |
