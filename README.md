# CUTE CHAT DOCUMENTATION

### Requirements

`ASP.NET Core SDK v8.0~`

### Steps for running the app

* #### Clone the github repository
```shell
git clone https://github.com/ramoneitor439/cute-chat-backend.git
```

* #### Run following commands
```shell
dotnet restore
dotnet build
dotnet run
```

# REPOSITORY STANDARDS

### Commits

* Each commit must only change from **1** to **10** files at most

* Each commit must refer only to **one** functionality

### All commits must be created following the next structure

`<type>(<layer>|<module>): "<description>"`

#### Type:

* `feat`: It is used for new changes, functionalities or entities
* `fix`: It is used for bug fixes or error correction.
* `refactor`: It is used to reimplement functionalities or improve the code.

#### Layer:

It refers to the layer where the change will be made.

`Examples`: `Domain`, `Infrastructure`, `Services`

#### Module:

Refers to a specific module within the selected layer where the change is made.

`Examples`: `Security`, `Entities`, `Results`, `AppUser`

#### Description:

A brief but explanatory description of the changes contained in the commit

`Example`: `Add new Hashing service with SHA256 algorithm to passwords and secret keys`

#### Final result:

`feat(Infrastructure|Encryption): Add new Hashing service with SHA256 algorithm to passwords and secret keys`





