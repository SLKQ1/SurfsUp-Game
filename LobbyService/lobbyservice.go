package main

import (
	"encoding/json"
	"fmt"
	"io/ioutil"
	"net/http"

	"github.com/gorilla/mux"
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

var lobbies = allLobbies{}

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

func main() {
	router := mux.NewRouter().StrictSlash(true)
	router.HandleFunc("/", lobbyService)
	router.HandleFunc("/createlobby", createLobby).Methods("POST")
	http.ListenAndServe(":8080", router)

}
