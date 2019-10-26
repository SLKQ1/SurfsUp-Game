package main

import (
	"encoding/json"
	"fmt"
	"io/ioutil"
	"net/http"
	"strconv"

	"github.com/gorilla/mux"
	"github.com/rs/xid"
)

// Represents a lobby room in the game
type lobby struct {
	ID             int    `json:"ID"`
	GameType       string `json:"GameType"`       // The type of game the lobby is hosting, ex. Teams/Solo.
	Joinable       bool   `json:"Joinable"`       // Whether or not the lobby is joinable
	IsStarted      bool   `json:"IsStarted"`      // Whether or not the lobby is already started and the players in-game
	CurrentPlayers []int  `json:"CurrentPlayers"` // The IDs of the current players in the lobby / game.
	MaximumPlayers int    `json:"MaximumPlayers"` // The maximum number of players allowed in the lobby / game.
}
type allLobbies []lobby

// Represents a player in the game
type player struct {
	ID          int    `json:"ID"`
	Token       string `json:"Token"`       // The Token of the player will serve as a secret between a player and the lobby service
	LobbyID     int    `json:"LobbyID"`     // The lobby the player is currently joined. 0 if player is not in any lobby.
	PlayerName  string `json:"PlayerName"`  // The name of the player shown in-game
	PlayerTeam  string `json:"PlayerTeam"`  // The team name or team color the player is currently on.
	PlayerReady bool   `json:"PlayerReady"` // Whether or not the player is ready to play whilst in a lobby.
}
type allPlayers []player

// Represents a headless unity server
type server struct {
	ID        int    `json:"ID"`
	LobbyID   int    `json:"LobbyID"`   // The lobby ID the server is started with
	IPAddress string `json:"IPAddress"` // The IPAddress of the unity game server
	Port      int    `json:"Port"`      // The Port of the unity game server
}
type allServers []server

var lobbies = allLobbies{}
var players = allPlayers{}

func lobbyService(w http.ResponseWriter, r *http.Request) {
	fmt.Fprintln(w, "Surfs Up! - Lobby Service")
	fmt.Fprintf(w, "Current Version: 1.0.0")
}

func createLobby(w http.ResponseWriter, r *http.Request) {
	reqBody, err := ioutil.ReadAll(r.Body)
	if err == nil {
		var newLobby lobby
		json.Unmarshal(reqBody, &newLobby)

		newLobby.ID = len(lobbies) + 1    // automatically set the appropriate lobby ID
		newLobby.Joinable = true          // a new lobby should be joinable
		newLobby.CurrentPlayers = []int{} // a new lobby should have no players in it by default

		lobbies = append(lobbies, newLobby)
		w.WriteHeader(http.StatusCreated)
		json.NewEncoder(w).Encode(newLobby)
	} else {
		w.WriteHeader(http.StatusPreconditionFailed)
	}
}

func createPlayer(w http.ResponseWriter, r *http.Request) {
	reqBody, err := ioutil.ReadAll(r.Body)
	if err == nil {
		var newPlayer player
		json.Unmarshal(reqBody, &newPlayer)

		newPlayer.ID = len(players) + 1      // automatically set the appropriate player ID
		newPlayer.Token = xid.New().String() // automatically generate and assign a secure token

		// Check if the lobby ID exists in the request, if it does attempt to add the player to that given lobby
		if newPlayer.LobbyID != 0 && idExist(newPlayer.LobbyID, len(lobbies)) {
			lobby := lobbies[newPlayer.LobbyID-1]
			if lobby.Joinable {
				lobby.CurrentPlayers = append(lobby.CurrentPlayers, newPlayer.ID)
				lobby.Joinable = len(lobby.CurrentPlayers) < lobby.MaximumPlayers
				lobbies[newPlayer.LobbyID-1] = lobby
			} else {
				newPlayer.LobbyID = 0 // reset the lobby id as the player wasn't able to successfully join a lobby
				newPlayer.PlayerReady = false
			}
		} else {
			newPlayer.LobbyID = 0
			newPlayer.PlayerReady = false
		}

		players = append(players, newPlayer)
		w.WriteHeader(http.StatusCreated)
		json.NewEncoder(w).Encode(newPlayer)
	} else {
		w.WriteHeader(http.StatusPreconditionFailed)
	}
}

func getAllLobbies(w http.ResponseWriter, r *http.Request) {
	json.NewEncoder(w).Encode(lobbies)
}

func getLobbyByID(w http.ResponseWriter, r *http.Request) {
	lobbyID, _ := strconv.Atoi(mux.Vars(r)["id"])
	if idExist(lobbyID, len(lobbies)) {
		w.WriteHeader(http.StatusOK)
		json.NewEncoder(w).Encode(lobbies[lobbyID-1])
	} else {
		w.WriteHeader(http.StatusNoContent)
	}
}

func getPlayerByID(w http.ResponseWriter, r *http.Request) {
	playerID, _ := strconv.Atoi(mux.Vars(r)["id"])
	if idExist(playerID, len(players)) {
		player := players[playerID-1]
		player.Token = "redacted" // filter player secret token
		w.WriteHeader(http.StatusOK)
		json.NewEncoder(w).Encode(player)
	} else {
		w.WriteHeader(http.StatusNoContent)
	}
}

func updatePlayerByID(w http.ResponseWriter, r *http.Request) {
	reqBody, err := ioutil.ReadAll(r.Body)

	if err == nil {
		playerID, _ := strconv.Atoi(mux.Vars(r)["id"])

		if idExist(playerID, len(players)) {
			currentPlayer := players[playerID-1]
			var playerUpdate player
			json.Unmarshal(reqBody, &playerUpdate)

			if playerUpdate.Token == currentPlayer.Token { // make sure the player is authenticated
				playerUpdate.ID = currentPlayer.ID // Make sure player ID is not modifed during the patch.

				// Lobby has been modified modify data structures accordingly
				if playerUpdate.LobbyID != currentPlayer.LobbyID {
					if playerUpdate.LobbyID == 0 { // The player has left a lobby
						lobby := lobbies[currentPlayer.LobbyID-1] // get the lobby the player is curently in

						// Remove the players ID from the lobby it's currently in
						i := 0
						for _, playerID := range lobby.CurrentPlayers {
							if playerID != currentPlayer.ID {
								lobby.CurrentPlayers[i] = playerID
								i++
							}
						}
						lobby.CurrentPlayers = lobby.CurrentPlayers[:i]
						lobby.Joinable = len(lobby.CurrentPlayers) < lobby.MaximumPlayers
						lobbies[currentPlayer.LobbyID-1] = lobby
					} else if currentPlayer.LobbyID == 0 { // The player wants to join a lobby
						if idExist(playerUpdate.LobbyID, len(lobbies)) {
							lobby := lobbies[playerUpdate.LobbyID-1]
							if lobby.Joinable {
								lobby.CurrentPlayers = append(lobby.CurrentPlayers, playerUpdate.ID)
								lobby.Joinable = len(lobby.CurrentPlayers) < lobby.MaximumPlayers
								lobbies[playerUpdate.LobbyID-1] = lobby
							} else {
								playerUpdate.LobbyID = 0
								playerUpdate.PlayerReady = false
							}
						}
					} else {
						// Dont change lobby as the player is trying to join a lobby without leaving the one they are currently in first.
						playerUpdate.LobbyID = currentPlayer.LobbyID
					}

				}
				players[playerID-1] = playerUpdate
				w.WriteHeader(http.StatusOK)
				json.NewEncoder(w).Encode(playerUpdate)
			} else {
				w.WriteHeader(http.StatusUnauthorized)
			}
		} else {
			w.WriteHeader(http.StatusNoContent)
		}
	} else {
		w.WriteHeader(http.StatusPreconditionFailed)
	}
}

func idExist(id, length int) bool {
	var offset int = id - 1 // Get the index offset
	return offset >= 0 && offset < length
}

func main() {
	router := mux.NewRouter().StrictSlash(true)
	router.HandleFunc("/", lobbyService)
	router.HandleFunc("/createlobby", createLobby).Methods("POST")
	router.HandleFunc("/createplayer", createPlayer).Methods("POST")
	router.HandleFunc("/lobbies", getAllLobbies).Methods("GET")
	router.HandleFunc("/lobbies/{id}", getLobbyByID).Methods("GET")
	router.HandleFunc("/players/{id}", getPlayerByID).Methods("GET")
	router.HandleFunc("/players/{id}", updatePlayerByID).Methods("PATCH")
	http.ListenAndServe(":8080", router)

}
