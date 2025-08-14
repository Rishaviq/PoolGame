
# Pool Game Api

This project is the beginning of a full web application designed to help me and my friends track our pool games. Have you ever wondered whether you’ve won more games than a friend? This app aims to solve that problem by storing and retrieving game statistics in a database. Currently, this repository contains only the backend, while a separate project will handle the frontend visualization.

---
title: Pool Game API v1
language_tabs:
  - "'http": HTTP'
  - "'javascript": JavaScript'
  - "'shell": Shell'
  - \: \
language_clients:
  - "'http": ""
  - "'javascript": ""
  - "'shell": ""


---

<!-- Generator: Widdershins v4.0.1 -->

<h1 id="your-api">Your API v1</h1>

> Scroll down for code samples, example requests and responses. Select a language for code samples from the tabs above or the mobile navigation menu.

API documentation with JWT auth

# Authentication

- HTTP Authentication, scheme: Bearer Enter your JWT token in this field

<h1 id="your-api-game">Game</h1>

## get__Game

> Code samples

`GET /Game`

> Body parameter

```json
"2019-08-24T14:15:22Z"
```

<h3 id="get__game-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|body|body|string(date-time)|false|none|

> Example responses

> 200 Response

```
{"isSuccesful":true,"message":"string","games":[{"gameId":0,"gameDate":"2019-08-24T14:15:22Z","gameIsDraw":true}]}
```

```json
{
  "isSuccesful": true,
  "message": "string",
  "games": [
    {
      "gameId": 0,
      "gameDate": "2019-08-24T14:15:22Z",
      "gameIsDraw": true
    }
  ]
}
```

<h3 id="get__game-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[PoolGame.Services.DTOs.Game.Response.GetGamesListResponse](#schemapoolgame.services.dtos.game.response.getgameslistresponse)|

<aside class="warning">
To perform this operation, you must be authenticated by means of one of the following methods:
Bearer
</aside>

## get__Game_{id}

> Code samples

`GET /Game/{id}`

<h3 id="get__game_{id}-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|id|path|integer(int32)|true|none|

> Example responses

> 200 Response

```
{"isSuccesful":true,"message":"string","game":{"gameId":0,"gameDate":"2019-08-24T14:15:22Z","gameIsDraw":true}}
```

```json
{
  "isSuccesful": true,
  "message": "string",
  "game": {
    "gameId": 0,
    "gameDate": "2019-08-24T14:15:22Z",
    "gameIsDraw": true
  }
}
```

<h3 id="get__game_{id}-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[PoolGame.Services.DTOs.Game.Response.GetGameResponse](#schemapoolgame.services.dtos.game.response.getgameresponse)|

<aside class="warning">
To perform this operation, you must be authenticated by means of one of the following methods:
Bearer
</aside>

## post__Game_create

> Code samples

`POST /Game/create`

> Example responses

> 200 Response

```
{"isSuccesful":true,"message":"string","gameId":0}
```

```json
{
  "isSuccesful": true,
  "message": "string",
  "gameId": 0
}
```

<h3 id="post__game_create-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[PoolGame.Services.DTOs.Game.Response.CreateGameResponse](#schemapoolgame.services.dtos.game.response.creategameresponse)|

<aside class="warning">
To perform this operation, you must be authenticated by means of one of the following methods:
Bearer
</aside>

<h1 id="your-api-login">LogIn</h1>

## post__log-in

> Code samples

`POST /log-in`

> Body parameter

```json
{
  "username": "string",
  "password": "string"
}
```

<h3 id="post__log-in-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|body|body|[PoolGame.WebAPI.Models.LogIn.Requests.LoginRequestDTO](#schemapoolgame.webapi.models.login.requests.loginrequestdto)|false|none|

<h3 id="post__log-in-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

<aside class="warning">
To perform this operation, you must be authenticated by means of one of the following methods:
Bearer
</aside>

## post__register

> Code samples

`POST /register`

> Body parameter

```json
{
  "username": "string",
  "profileName": "string",
  "userPassword": "string"
}
```

<h3 id="post__register-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|body|body|[PoolGame.WebAPI.Models.LogIn.Requests.RegisterRequestDTO](#schemapoolgame.webapi.models.login.requests.registerrequestdto)|false|none|

<h3 id="post__register-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

<aside class="warning">
To perform this operation, you must be authenticated by means of one of the following methods:
Bearer
</aside>

<h1 id="your-api-playerstats">PlayerStats</h1>

## get__player_{id}_stats

> Code samples

`GET /player/{id}/stats`

<h3 id="get__player_{id}_stats-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|id|path|integer(int32)|true|none|

> Example responses

> 200 Response

```
{"isSuccesful":true,"message":"string","playerWinRate":0.1,"totalGamesPlayed":0,"totalGamesWon":0,"totalGamesLost":0,"shotSuccessRate":0.1,"totalShotsMade":0,"totalShotsAttempted":0,"averageHandBalls":0.1,"averageFouls":0.1,"bestStreak":0}
```

```json
{
  "isSuccesful": true,
  "message": "string",
  "playerWinRate": 0.1,
  "totalGamesPlayed": 0,
  "totalGamesWon": 0,
  "totalGamesLost": 0,
  "shotSuccessRate": 0.1,
  "totalShotsMade": 0,
  "totalShotsAttempted": 0,
  "averageHandBalls": 0.1,
  "averageFouls": 0.1,
  "bestStreak": 0
}
```

<h3 id="get__player_{id}_stats-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[PoolGame.Services.DTOs.PlayerStat.Response.GetPersonalStatsResponse](#schemapoolgame.services.dtos.playerstat.response.getpersonalstatsresponse)|

<aside class="warning">
To perform this operation, you must be authenticated by means of one of the following methods:
Bearer
</aside>

## get__player_{id}_history

> Code samples

`GET /player/{id}/history`

<h3 id="get__player_{id}_history-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|id|path|integer(int32)|true|none|

> Example responses

> 200 Response

```
{"isSuccesful":true,"message":"string","playerGames":[{"gameId":0,"gameDate":"2019-08-24T14:15:22Z","gameIsDraw":true,"playerId":0,"isPlayerWinner":true}]}
```

```json
{
  "isSuccesful": true,
  "message": "string",
  "playerGames": [
    {
      "gameId": 0,
      "gameDate": "2019-08-24T14:15:22Z",
      "gameIsDraw": true,
      "playerId": 0,
      "isPlayerWinner": true
    }
  ]
}
```

<h3 id="get__player_{id}_history-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[PoolGame.Services.DTOs.PlayerStat.Response.GetMatchHistoryResponse](#schemapoolgame.services.dtos.playerstat.response.getmatchhistoryresponse)|

<aside class="warning">
To perform this operation, you must be authenticated by means of one of the following methods:
Bearer
</aside>

## get__player_game-stats_{gameId}

> Code samples

`GET /player/game-stats/{gameId}`

<h3 id="get__player_game-stats_{gameid}-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|gameId|path|integer(int32)|true|none|

> Example responses

> 200 Response

```
{"isSuccesful":true,"message":"string","playerStats":[{"userId":0,"userName":"string","isWinner":true,"shotsMade":0,"shotsAttempted":0,"handBalls":0,"fouls":0,"bestStreak":0}],"gameInfo":{"gameId":0,"gameDate":"2019-08-24T14:15:22Z","gameIsDraw":true}}
```

```json
{
  "isSuccesful": true,
  "message": "string",
  "playerStats": [
    {
      "userId": 0,
      "userName": "string",
      "isWinner": true,
      "shotsMade": 0,
      "shotsAttempted": 0,
      "handBalls": 0,
      "fouls": 0,
      "bestStreak": 0
    }
  ],
  "gameInfo": {
    "gameId": 0,
    "gameDate": "2019-08-24T14:15:22Z",
    "gameIsDraw": true
  }
}
```

<h3 id="get__player_game-stats_{gameid}-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[PoolGame.Services.DTOs.PlayerStat.Response.GetPlayerGameStatsResponse](#schemapoolgame.services.dtos.playerstat.response.getplayergamestatsresponse)|

<aside class="warning">
To perform this operation, you must be authenticated by means of one of the following methods:
Bearer
</aside>

## post__player_stats_save

> Code samples

`POST /player/stats/save`

> Body parameter

```json
{
  "gameId": 0,
  "userId": 0,
  "isWinner": true,
  "shotsMade": 15,
  "shotsAttempted": 2147483647,
  "handBalls": 2147483647,
  "fouls": 2147483647,
  "bestStreak": 15
}
```

<h3 id="post__player_stats_save-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|body|body|[PoolGame.Services.DTOs.PlayerStat.Request.SaveStatsRequest](#schemapoolgame.services.dtos.playerstat.request.savestatsrequest)|false|none|

> Example responses

> 200 Response

```
{"isSuccesful":true,"message":"string"}
```

```json
{
  "isSuccesful": true,
  "message": "string"
}
```

<h3 id="post__player_stats_save-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[PoolGame.Services.DTOs.PlayerStat.Response.SaveStatsResponse](#schemapoolgame.services.dtos.playerstat.response.savestatsresponse)|

<aside class="warning">
To perform this operation, you must be authenticated by means of one of the following methods:
Bearer
</aside>

## get__player_leaderboard

> Code samples

`GET /player/leaderboard`

> Example responses

> 200 Response

```
{"isSuccesful":true,"message":"string","leaderboardEntries":[{"userId":0,"userName":"string","winRate":0.1,"totalGames":0,"gamesWon":0,"gamesLost":0}]}
```

```json
{
  "isSuccesful": true,
  "message": "string",
  "leaderboardEntries": [
    {
      "userId": 0,
      "userName": "string",
      "winRate": 0.1,
      "totalGames": 0,
      "gamesWon": 0,
      "gamesLost": 0
    }
  ]
}
```

<h3 id="get__player_leaderboard-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[PoolGame.Services.DTOs.PlayerStat.Response.GetLeaderboardResponse](#schemapoolgame.services.dtos.playerstat.response.getleaderboardresponse)|

<aside class="warning">
To perform this operation, you must be authenticated by means of one of the following methods:
Bearer
</aside>

# Schemas

<h2 id="tocS_PoolGame.Services.DTOs.Game.GameDTO">PoolGame.Services.DTOs.Game.GameDTO</h2>
<!-- backwards compatibility -->
<a id="schemapoolgame.services.dtos.game.gamedto"></a>
<a id="schema_PoolGame.Services.DTOs.Game.GameDTO"></a>
<a id="tocSpoolgame.services.dtos.game.gamedto"></a>
<a id="tocspoolgame.services.dtos.game.gamedto"></a>

```json
{
  "gameId": 0,
  "gameDate": "2019-08-24T14:15:22Z",
  "gameIsDraw": true
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|gameId|integer(int32)|false|none|none|
|gameDate|string(date-time)|true|none|none|
|gameIsDraw|boolean|true|none|none|

<h2 id="tocS_PoolGame.Services.DTOs.Game.Response.CreateGameResponse">PoolGame.Services.DTOs.Game.Response.CreateGameResponse</h2>
<!-- backwards compatibility -->
<a id="schemapoolgame.services.dtos.game.response.creategameresponse"></a>
<a id="schema_PoolGame.Services.DTOs.Game.Response.CreateGameResponse"></a>
<a id="tocSpoolgame.services.dtos.game.response.creategameresponse"></a>
<a id="tocspoolgame.services.dtos.game.response.creategameresponse"></a>

```json
{
  "isSuccesful": true,
  "message": "string",
  "gameId": 0
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|isSuccesful|boolean|false|none|none|
|message|string¦null|false|none|none|
|gameId|integer(int32)|false|none|none|

<h2 id="tocS_PoolGame.Services.DTOs.Game.Response.GetGameResponse">PoolGame.Services.DTOs.Game.Response.GetGameResponse</h2>
<!-- backwards compatibility -->
<a id="schemapoolgame.services.dtos.game.response.getgameresponse"></a>
<a id="schema_PoolGame.Services.DTOs.Game.Response.GetGameResponse"></a>
<a id="tocSpoolgame.services.dtos.game.response.getgameresponse"></a>
<a id="tocspoolgame.services.dtos.game.response.getgameresponse"></a>

```json
{
  "isSuccesful": true,
  "message": "string",
  "game": {
    "gameId": 0,
    "gameDate": "2019-08-24T14:15:22Z",
    "gameIsDraw": true
  }
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|isSuccesful|boolean|false|none|none|
|message|string¦null|false|none|none|
|game|[PoolGame.Services.DTOs.Game.GameDTO](#schemapoolgame.services.dtos.game.gamedto)|false|none|none|

<h2 id="tocS_PoolGame.Services.DTOs.Game.Response.GetGamesListResponse">PoolGame.Services.DTOs.Game.Response.GetGamesListResponse</h2>
<!-- backwards compatibility -->
<a id="schemapoolgame.services.dtos.game.response.getgameslistresponse"></a>
<a id="schema_PoolGame.Services.DTOs.Game.Response.GetGamesListResponse"></a>
<a id="tocSpoolgame.services.dtos.game.response.getgameslistresponse"></a>
<a id="tocspoolgame.services.dtos.game.response.getgameslistresponse"></a>

```json
{
  "isSuccesful": true,
  "message": "string",
  "games": [
    {
      "gameId": 0,
      "gameDate": "2019-08-24T14:15:22Z",
      "gameIsDraw": true
    }
  ]
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|isSuccesful|boolean|false|none|none|
|message|string¦null|false|none|none|
|games|[[PoolGame.Services.DTOs.Game.GameDTO](#schemapoolgame.services.dtos.game.gamedto)]¦null|false|none|none|

<h2 id="tocS_PoolGame.Services.DTOs.PlayerStat.LeaderBoardEntryDTO">PoolGame.Services.DTOs.PlayerStat.LeaderBoardEntryDTO</h2>
<!-- backwards compatibility -->
<a id="schemapoolgame.services.dtos.playerstat.leaderboardentrydto"></a>
<a id="schema_PoolGame.Services.DTOs.PlayerStat.LeaderBoardEntryDTO"></a>
<a id="tocSpoolgame.services.dtos.playerstat.leaderboardentrydto"></a>
<a id="tocspoolgame.services.dtos.playerstat.leaderboardentrydto"></a>

```json
{
  "userId": 0,
  "userName": "string",
  "winRate": 0.1,
  "totalGames": 0,
  "gamesWon": 0,
  "gamesLost": 0
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|userId|integer(int32)|false|none|none|
|userName|string¦null|false|none|none|
|winRate|number(double)|false|none|none|
|totalGames|integer(int32)|false|none|none|
|gamesWon|integer(int32)|false|none|none|
|gamesLost|integer(int32)|false|none|none|

<h2 id="tocS_PoolGame.Services.DTOs.PlayerStat.PlayerGameDTO">PoolGame.Services.DTOs.PlayerStat.PlayerGameDTO</h2>
<!-- backwards compatibility -->
<a id="schemapoolgame.services.dtos.playerstat.playergamedto"></a>
<a id="schema_PoolGame.Services.DTOs.PlayerStat.PlayerGameDTO"></a>
<a id="tocSpoolgame.services.dtos.playerstat.playergamedto"></a>
<a id="tocspoolgame.services.dtos.playerstat.playergamedto"></a>

```json
{
  "gameId": 0,
  "gameDate": "2019-08-24T14:15:22Z",
  "gameIsDraw": true,
  "playerId": 0,
  "isPlayerWinner": true
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|gameId|integer(int32)|false|none|none|
|gameDate|string(date-time)|true|none|none|
|gameIsDraw|boolean|true|none|none|
|playerId|integer(int32)|false|none|none|
|isPlayerWinner|boolean|false|none|none|

<h2 id="tocS_PoolGame.Services.DTOs.PlayerStat.PlayerStatDTO">PoolGame.Services.DTOs.PlayerStat.PlayerStatDTO</h2>
<!-- backwards compatibility -->
<a id="schemapoolgame.services.dtos.playerstat.playerstatdto"></a>
<a id="schema_PoolGame.Services.DTOs.PlayerStat.PlayerStatDTO"></a>
<a id="tocSpoolgame.services.dtos.playerstat.playerstatdto"></a>
<a id="tocspoolgame.services.dtos.playerstat.playerstatdto"></a>

```json
{
  "userId": 0,
  "userName": "string",
  "isWinner": true,
  "shotsMade": 0,
  "shotsAttempted": 0,
  "handBalls": 0,
  "fouls": 0,
  "bestStreak": 0
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|userId|integer(int32)|false|none|none|
|userName|string¦null|false|none|none|
|isWinner|boolean|false|none|none|
|shotsMade|integer(int32)|false|none|none|
|shotsAttempted|integer(int32)|false|none|none|
|handBalls|integer(int32)|false|none|none|
|fouls|integer(int32)|false|none|none|
|bestStreak|integer(int32)|false|none|none|

<h2 id="tocS_PoolGame.Services.DTOs.PlayerStat.Request.SaveStatsRequest">PoolGame.Services.DTOs.PlayerStat.Request.SaveStatsRequest</h2>
<!-- backwards compatibility -->
<a id="schemapoolgame.services.dtos.playerstat.request.savestatsrequest"></a>
<a id="schema_PoolGame.Services.DTOs.PlayerStat.Request.SaveStatsRequest"></a>
<a id="tocSpoolgame.services.dtos.playerstat.request.savestatsrequest"></a>
<a id="tocspoolgame.services.dtos.playerstat.request.savestatsrequest"></a>

```json
{
  "gameId": 0,
  "userId": 0,
  "isWinner": true,
  "shotsMade": 15,
  "shotsAttempted": 2147483647,
  "handBalls": 2147483647,
  "fouls": 2147483647,
  "bestStreak": 15
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|gameId|integer(int32)|true|none|none|
|userId|integer(int32)|true|none|none|
|isWinner|boolean|true|none|none|
|shotsMade|integer(int32)|false|none|none|
|shotsAttempted|integer(int32)|false|none|none|
|handBalls|integer(int32)|false|none|none|
|fouls|integer(int32)|false|none|none|
|bestStreak|integer(int32)|false|none|none|

<h2 id="tocS_PoolGame.Services.DTOs.PlayerStat.Response.GetLeaderboardResponse">PoolGame.Services.DTOs.PlayerStat.Response.GetLeaderboardResponse</h2>
<!-- backwards compatibility -->
<a id="schemapoolgame.services.dtos.playerstat.response.getleaderboardresponse"></a>
<a id="schema_PoolGame.Services.DTOs.PlayerStat.Response.GetLeaderboardResponse"></a>
<a id="tocSpoolgame.services.dtos.playerstat.response.getleaderboardresponse"></a>
<a id="tocspoolgame.services.dtos.playerstat.response.getleaderboardresponse"></a>

```json
{
  "isSuccesful": true,
  "message": "string",
  "leaderboardEntries": [
    {
      "userId": 0,
      "userName": "string",
      "winRate": 0.1,
      "totalGames": 0,
      "gamesWon": 0,
      "gamesLost": 0
    }
  ]
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|isSuccesful|boolean|false|none|none|
|message|string¦null|false|none|none|
|leaderboardEntries|[[PoolGame.Services.DTOs.PlayerStat.LeaderBoardEntryDTO](#schemapoolgame.services.dtos.playerstat.leaderboardentrydto)]¦null|false|none|none|

<h2 id="tocS_PoolGame.Services.DTOs.PlayerStat.Response.GetMatchHistoryResponse">PoolGame.Services.DTOs.PlayerStat.Response.GetMatchHistoryResponse</h2>
<!-- backwards compatibility -->
<a id="schemapoolgame.services.dtos.playerstat.response.getmatchhistoryresponse"></a>
<a id="schema_PoolGame.Services.DTOs.PlayerStat.Response.GetMatchHistoryResponse"></a>
<a id="tocSpoolgame.services.dtos.playerstat.response.getmatchhistoryresponse"></a>
<a id="tocspoolgame.services.dtos.playerstat.response.getmatchhistoryresponse"></a>

```json
{
  "isSuccesful": true,
  "message": "string",
  "playerGames": [
    {
      "gameId": 0,
      "gameDate": "2019-08-24T14:15:22Z",
      "gameIsDraw": true,
      "playerId": 0,
      "isPlayerWinner": true
    }
  ]
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|isSuccesful|boolean|false|none|none|
|message|string¦null|false|none|none|
|playerGames|[[PoolGame.Services.DTOs.PlayerStat.PlayerGameDTO](#schemapoolgame.services.dtos.playerstat.playergamedto)]¦null|false|none|none|

<h2 id="tocS_PoolGame.Services.DTOs.PlayerStat.Response.GetPersonalStatsResponse">PoolGame.Services.DTOs.PlayerStat.Response.GetPersonalStatsResponse</h2>
<!-- backwards compatibility -->
<a id="schemapoolgame.services.dtos.playerstat.response.getpersonalstatsresponse"></a>
<a id="schema_PoolGame.Services.DTOs.PlayerStat.Response.GetPersonalStatsResponse"></a>
<a id="tocSpoolgame.services.dtos.playerstat.response.getpersonalstatsresponse"></a>
<a id="tocspoolgame.services.dtos.playerstat.response.getpersonalstatsresponse"></a>

```json
{
  "isSuccesful": true,
  "message": "string",
  "playerWinRate": 0.1,
  "totalGamesPlayed": 0,
  "totalGamesWon": 0,
  "totalGamesLost": 0,
  "shotSuccessRate": 0.1,
  "totalShotsMade": 0,
  "totalShotsAttempted": 0,
  "averageHandBalls": 0.1,
  "averageFouls": 0.1,
  "bestStreak": 0
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|isSuccesful|boolean|false|none|none|
|message|string¦null|false|none|none|
|playerWinRate|number(double)¦null|false|none|none|
|totalGamesPlayed|integer(int32)¦null|false|none|none|
|totalGamesWon|integer(int32)¦null|false|none|none|
|totalGamesLost|integer(int32)¦null|false|none|none|
|shotSuccessRate|number(double)¦null|false|none|none|
|totalShotsMade|integer(int32)¦null|false|none|none|
|totalShotsAttempted|integer(int32)¦null|false|none|none|
|averageHandBalls|number(double)¦null|false|none|none|
|averageFouls|number(double)¦null|false|none|none|
|bestStreak|integer(int32)¦null|false|none|none|

<h2 id="tocS_PoolGame.Services.DTOs.PlayerStat.Response.GetPlayerGameStatsResponse">PoolGame.Services.DTOs.PlayerStat.Response.GetPlayerGameStatsResponse</h2>
<!-- backwards compatibility -->
<a id="schemapoolgame.services.dtos.playerstat.response.getplayergamestatsresponse"></a>
<a id="schema_PoolGame.Services.DTOs.PlayerStat.Response.GetPlayerGameStatsResponse"></a>
<a id="tocSpoolgame.services.dtos.playerstat.response.getplayergamestatsresponse"></a>
<a id="tocspoolgame.services.dtos.playerstat.response.getplayergamestatsresponse"></a>

```json
{
  "isSuccesful": true,
  "message": "string",
  "playerStats": [
    {
      "userId": 0,
      "userName": "string",
      "isWinner": true,
      "shotsMade": 0,
      "shotsAttempted": 0,
      "handBalls": 0,
      "fouls": 0,
      "bestStreak": 0
    }
  ],
  "gameInfo": {
    "gameId": 0,
    "gameDate": "2019-08-24T14:15:22Z",
    "gameIsDraw": true
  }
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|isSuccesful|boolean|false|none|none|
|message|string¦null|false|none|none|
|playerStats|[[PoolGame.Services.DTOs.PlayerStat.PlayerStatDTO](#schemapoolgame.services.dtos.playerstat.playerstatdto)]¦null|false|none|none|
|gameInfo|[PoolGame.Services.DTOs.Game.GameDTO](#schemapoolgame.services.dtos.game.gamedto)|false|none|none|

<h2 id="tocS_PoolGame.Services.DTOs.PlayerStat.Response.SaveStatsResponse">PoolGame.Services.DTOs.PlayerStat.Response.SaveStatsResponse</h2>
<!-- backwards compatibility -->
<a id="schemapoolgame.services.dtos.playerstat.response.savestatsresponse"></a>
<a id="schema_PoolGame.Services.DTOs.PlayerStat.Response.SaveStatsResponse"></a>
<a id="tocSpoolgame.services.dtos.playerstat.response.savestatsresponse"></a>
<a id="tocspoolgame.services.dtos.playerstat.response.savestatsresponse"></a>

```json
{
  "isSuccesful": true,
  "message": "string"
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|isSuccesful|boolean|false|none|none|
|message|string¦null|false|none|none|

<h2 id="tocS_PoolGame.WebAPI.Models.LogIn.Requests.LoginRequestDTO">PoolGame.WebAPI.Models.LogIn.Requests.LoginRequestDTO</h2>
<!-- backwards compatibility -->
<a id="schemapoolgame.webapi.models.login.requests.loginrequestdto"></a>
<a id="schema_PoolGame.WebAPI.Models.LogIn.Requests.LoginRequestDTO"></a>
<a id="tocSpoolgame.webapi.models.login.requests.loginrequestdto"></a>
<a id="tocspoolgame.webapi.models.login.requests.loginrequestdto"></a>

```json
{
  "username": "string",
  "password": "string"
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|username|string|true|none|none|
|password|string|true|none|none|

<h2 id="tocS_PoolGame.WebAPI.Models.LogIn.Requests.RegisterRequestDTO">PoolGame.WebAPI.Models.LogIn.Requests.RegisterRequestDTO</h2>
<!-- backwards compatibility -->
<a id="schemapoolgame.webapi.models.login.requests.registerrequestdto"></a>
<a id="schema_PoolGame.WebAPI.Models.LogIn.Requests.RegisterRequestDTO"></a>
<a id="tocSpoolgame.webapi.models.login.requests.registerrequestdto"></a>
<a id="tocspoolgame.webapi.models.login.requests.registerrequestdto"></a>

```json
{
  "username": "string",
  "profileName": "string",
  "userPassword": "string"
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|username|string|true|none|none|
|profileName|string¦null|false|none|none|
|userPassword|string|true|none|none|

