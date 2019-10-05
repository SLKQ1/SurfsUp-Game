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
	CurrentPlayers int    `json:"CurrentPlayers"` // The current number of players in the lobby / game.
	MaximumPlayers int    `json:"MaximumPlayers"` // The maximum number of players allowed in the lobby / game.
}
type allLobbies []lobby

// Represents players in the game
type player struct {
	ID          int    `json:"ID"`
	Token       string `json:"Token"`       // The Token of the player will serve as a secret between a player and the lobby service
	LobbyID     int    `json:"LobbyID"`     // The lobby the player is currently joined. 0 if player is not in any lobby.
	PlayerName  string `json:"PlayerName"`  // The name of the player shown in-game
	PlayerTeam  string `json:"PlayerTeam"`  // The team name or team color the player is currently on.
	PlayerReady bool   `json:"PlayerReady"` // Whether or not the player is ready to play whilst in a lobby.
}
type allPlayers []player

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

		newLobby.ID = len(lobbies) + 1 // automatically set the appropriate lobby ID
		newLobby.Joinable = true       // a new lobby should be joinable
		newLobby.CurrentPlayers = 0    // a new lobby should have no players in it by default

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
	found := false // Whether or not we found the target lobby in our array
	for _, lobby := range lobbies {
		found = lobby.ID == lobbyID
		if found {
			w.WriteHeader(http.StatusOK)
			json.NewEncoder(w).Encode(lobby)
		}
	}
	if !found {
		w.WriteHeader(http.StatusNoContent)
	}
}

func main() {
	router := mux.NewRouter().StrictSlash(true)
	router.HandleFunc("/", lobbyService)
	router.HandleFunc("/createlobby", createLobby).Methods("POST")
	router.HandleFunc("/createplayer", createPlayer).Methods("POST")
	router.HandleFunc("/lobbies", getAllLobbies).Methods("GET")
	router.HandleFunc("/lobbies/{id}", getLobbyByID).Methods("GET")
	http.ListenAndServe(":8080", router)

}
