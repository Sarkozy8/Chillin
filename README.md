### Development of Chillin' Demo

Big files like PNG, MP3, MP4, TIF, are not being tracked but their META files are.

For Third parties Assets, get their respective packages. For local Assets, get their respective locally.

Before rolling back to check for errors, have a copy of the "current version assets" and a copy of the "previous assets".
When rolling back, replace assets with the copy of the "previous assets", check where the errors is, and then roll back to the current version, replace assets with current version assets and then you can start working on the error. Never work on the error in previous branches since the assets when merging are not going to be the same.

For the future, I need to find something more relaible for storing all the assets, including big ones.
