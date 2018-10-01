# Gamebase Push FCM Adapter for Android

## Dependencies

| Module | Dependencies | Description |
| ------- | ------------ | ----------- |
| gamebase-adapter-push-fcm | gamebase-sdk-base-{version}.aar | Gamebase Android SDK |
| | pushsdk-{version}.aar | TOAST Cloud Push Android SDK |
| | play-services-gcm-{version}.aar<br>firebase-messaging-{version}.aar | pushsdk dependancies :<br>Google Cloud Messaging<br>Firebase Cloud Messaging |
| | play-services-base-{version}.aar<br>play-services-basement-{version}.aar<br>play-services-iid-{version}.aar | play-services-gcm dependancies |
| | play-services-basement-{version}.aar<br>play-services-tasks-{version}.aar | play-services-base dependencies |
| | play-services-basement-{version}.aar | play-services-tasks dependencies |
| | support-v4-{version}.aar | play-services-basement dependencies |
| | support-compat-{version}.aar<br>support-media-compat-{version}.aar<br>support-core-utils-{version}.aar<br>support-core-ui-{version}.aar<br>support-fragment-{version}.aar | support-v4 dependencies |
| | support-annotations-{version}.jar<br>android.arch.lifecycle:runtime-1.0.3.aar | support-compat dependencies |
| | android.arch.lifecycle:common-1.0.3.jar<br>android.arch.core:common-1.0.0.jar<br>support-annotations-{version}.jar | runtime dependencies |
| | support-annotations-{version}.jar<br>support-compat-{version}.aar | support-media-compat dependencies |
| | support-annotations-{version}.jar<br>support-compat-{version}.aar | support-core-utils dependencies |
| | support-annotations-{version}.jar<br>support-compat-{version}.aar | support-core-ui dependencies |
| | support-compat-{version}.aar<br>support-core-ui-{version}.aar<br>support-core-utils-{version}.aar<br>support-annotations-{version}.jar | support-fragment dependencies |
| | play-services-base-{version}.aar<br>play-services-basement-{version}.aar | play-services-iid dependancies |
| | firebase-iid-{version}.aar<br>play-services-basement-{version}.aar<br>firebase-common-{version}.aar | firebase-messaging dependancies |
| | play-services-basement-{version}.aar<br>firebase-common-{version}.aar<br>play-services-tasks-{version}.aar | firebase-iid dependancies |
| | play-services-basement-{version}.aar<br>play-services-tasks-{version}.aar | firebase-common dependancies |

### Dependencies of Google Play Services libraries
* https://developers.google.com/android/guides/setup#add_google_play_services_to_your_project

### Dependencies of Firebase libraries
* https://firebase.google.com/docs/android/setup#available_libraries

## Release Note

| Date | FCM Adapter Version | Gamebase SDK Version | TCPush SDK Version | Description |
| --- | --- | --- | --- | --- |
| 2017.03.09 | 1.0.0 | 1.0.0 ~ 1.1.3.1 | 1.32 | - Initial version |
| 2017.04.28 | 1.1.3.2 | 1.1.3.2 | 1.4.0 | - Update TCPush SDK to 1.4.0 |
| 2017.05.23 | 1.1.4   | 1.1.4 ~ 1.1.4.1 | 1.4.0 | - Update version |
| 2017.06.27 | 1.1.4.2 | 1.1.4.2 ~ 1.5.0 | 1.4.1 ~ 1.4.2 | - Update TCPush SDK to 1.4.1 |
| 2017.07.19 | 1.1.5   | 1.1.5 ~ 1.5.0 | 1.4.1 ~ 1.4.2 | - Update version |
| 2018.02.22 | 1.7.0   | 1.7.0 ~  | 1.4.1 ~ 1.4.2 | - Update TCPush SDK to 1.4.2<br>- Calling PushAnalytics.onReceived() API |