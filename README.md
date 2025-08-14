
# Pool Game Api

This project is the beginning of a full web application designed to help me and my friends track our pool games. Have you ever wondered whether you’ve won more games than a friend? This app aims to solve that problem by storing and retrieving game statistics in a database. Currently, this repository contains only the backend, while a separate project will handle the frontend visualization.

# PoolGame API References
API documentation with JWT auth

## Version: v1

### /Game

#### GET
##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | OK |

### /Game/{id}

#### GET
##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| id | path |  | Yes | integer |

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | OK |

### /Game/create

#### POST
##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | OK |

### /log-in

#### POST
##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | OK |

### /register

#### POST
##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | OK |

### /player/{id}/stats

#### GET
##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| id | path |  | Yes | integer |

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | OK |

### /player/{id}/history

#### GET
##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| id | path |  | Yes | integer |

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | OK |

### /player/game-stats/{gameId}

#### GET
##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| gameId | path |  | Yes | integer |

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | OK |

### /player/stats/save

#### POST
##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | OK |

### /player/leaderboard

#### GET
##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | OK |

### Models


#### PoolGame.Services.DTOs.Game.GameDTO

| Name | Type | Description | Required |
| ---- | ---- | ----------- | -------- |
| gameId | integer |  | No |
| gameDate | dateTime |  | Yes |
| gameIsDraw | boolean |  | Yes |

#### PoolGame.Services.DTOs.Game.Response.CreateGameResponse

| Name | Type | Description | Required |
| ---- | ---- | ----------- | -------- |
| isSuccesful | boolean |  | No |
| message | string |  | No |
| gameId | integer |  | No |

#### PoolGame.Services.DTOs.Game.Response.GetGameResponse

| Name | Type | Description | Required |
| ---- | ---- | ----------- | -------- |
| isSuccesful | boolean |  | No |
| message | string |  | No |
| game | [PoolGame.Services.DTOs.Game.GameDTO](#poolgame.services.dtos.game.gamedto) |  | No |

#### PoolGame.Services.DTOs.Game.Response.GetGamesListResponse

| Name | Type | Description | Required |
| ---- | ---- | ----------- | -------- |
| isSuccesful | boolean |  | No |
| message | string |  | No |
| games | [ [PoolGame.Services.DTOs.Game.GameDTO](#poolgame.services.dtos.game.gamedto) ] |  | No |

#### PoolGame.Services.DTOs.PlayerStat.LeaderBoardEntryDTO

| Name | Type | Description | Required |
| ---- | ---- | ----------- | -------- |
| userId | integer |  | No |
| userName | string |  | No |
| winRate | double |  | No |
| totalGames | integer |  | No |
| gamesWon | integer |  | No |
| gamesLost | integer |  | No |

#### PoolGame.Services.DTOs.PlayerStat.PlayerGameDTO

| Name | Type | Description | Required |
| ---- | ---- | ----------- | -------- |
| gameId | integer |  | No |
| gameDate | dateTime |  | Yes |
| gameIsDraw | boolean |  | Yes |
| playerId | integer |  | No |
| isPlayerWinner | boolean |  | No |

#### PoolGame.Services.DTOs.PlayerStat.PlayerStatDTO

| Name | Type | Description | Required |
| ---- | ---- | ----------- | -------- |
| userId | integer |  | No |
| userName | string |  | No |
| isWinner | boolean |  | No |
| shotsMade | integer |  | No |
| shotsAttempted | integer |  | No |
| handBalls | integer |  | No |
| fouls | integer |  | No |
| bestStreak | integer |  | No |

#### PoolGame.Services.DTOs.PlayerStat.Request.SaveStatsRequest

| Name | Type | Description | Required |
| ---- | ---- | ----------- | -------- |
| gameId | integer |  | Yes |
| userId | integer |  | Yes |
| isWinner | boolean |  | Yes |
| shotsMade | integer |  | No |
| shotsAttempted | integer |  | No |
| handBalls | integer |  | No |
| fouls | integer |  | No |
| bestStreak | integer |  | No |

#### PoolGame.Services.DTOs.PlayerStat.Response.GetLeaderboardResponse

| Name | Type | Description | Required |
| ---- | ---- | ----------- | -------- |
| isSuccesful | boolean |  | No |
| message | string |  | No |
| leaderboardEntries | [ [PoolGame.Services.DTOs.PlayerStat.LeaderBoardEntryDTO](#poolgame.services.dtos.playerstat.leaderboardentrydto) ] |  | No |

#### PoolGame.Services.DTOs.PlayerStat.Response.GetMatchHistoryResponse

| Name | Type | Description | Required |
| ---- | ---- | ----------- | -------- |
| isSuccesful | boolean |  | No |
| message | string |  | No |
| playerGames | [ [PoolGame.Services.DTOs.PlayerStat.PlayerGameDTO](#poolgame.services.dtos.playerstat.playergamedto) ] |  | No |

#### PoolGame.Services.DTOs.PlayerStat.Response.GetPersonalStatsResponse

| Name | Type | Description | Required |
| ---- | ---- | ----------- | -------- |
| isSuccesful | boolean |  | No |
| message | string |  | No |
| playerWinRate | double |  | No |
| totalGamesPlayed | integer |  | No |
| totalGamesWon | integer |  | No |
| totalGamesLost | integer |  | No |
| shotSuccessRate | double |  | No |
| totalShotsMade | integer |  | No |
| totalShotsAttempted | integer |  | No |
| averageHandBalls | double |  | No |
| averageFouls | double |  | No |
| bestStreak | integer |  | No |

#### PoolGame.Services.DTOs.PlayerStat.Response.GetPlayerGameStatsResponse

| Name | Type | Description | Required |
| ---- | ---- | ----------- | -------- |
| isSuccesful | boolean |  | No |
| message | string |  | No |
| playerStats | [ [PoolGame.Services.DTOs.PlayerStat.PlayerStatDTO](#poolgame.services.dtos.playerstat.playerstatdto) ] |  | No |
| gameInfo | [PoolGame.Services.DTOs.Game.GameDTO](#poolgame.services.dtos.game.gamedto) |  | No |

#### PoolGame.Services.DTOs.PlayerStat.Response.SaveStatsResponse

| Name | Type | Description | Required |
| ---- | ---- | ----------- | -------- |
| isSuccesful | boolean |  | No |
| message | string |  | No |

#### PoolGame.WebAPI.Models.LogIn.Requests.LoginRequestDTO

| Name | Type | Description | Required |
| ---- | ---- | ----------- | -------- |
| username | string |  | Yes |
| password | string |  | Yes |

#### PoolGame.WebAPI.Models.LogIn.Requests.RegisterRequestDTO

| Name | Type | Description | Required |
| ---- | ---- | ----------- | -------- |
| username | string |  | Yes |
| profileName | string |  | No |
| userPassword | string |  | Yes |
