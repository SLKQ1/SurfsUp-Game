package routes

import (
	"encoding/json"
	"net/http"
	"strconv"

	"github.com/gorilla/mux"
)

// Represents a headless unity server
type server struct {
	ID        int    `json:"ID"`
	LobbyID   int    `json:"LobbyID"`   // The lobby ID the server is started with
	IPAddress string `json:"IPAddress"` // The IPAddress of the unity game server
	Port      int    `json:"Port"`      // The Port of the unity game server
}
type allServers []server

var servers = allServers{}

func getServerByLobbyID(w http.ResponseWriter, r *http.Request) {
	lobbyID, _ := strconv.Atoi(mux.Vars(r)["id"])

	if lobbyID > 0 && lobbyID <= len(lobbies) {
		lobby := lobbies[lobbyID-1]
		if len(lobby.CurrentPlayers) == lobby.MaximumPlayers {

			// Try getting the server based on the lobbyID
			var unityServer *server
			for _, server := range servers {
				if server.LobbyID == lobbyID {
					unityServer = &server
				}
			}

			// Create a new server
			if unityServer == nil {

				// TODO Launch a Headless Unity Server

				unityServer = &server{
					ID:        len(servers) + 1,
					LobbyID:   lobbyID,
					IPAddress: "0.0.0.0",
					Port:      12345,
				}

				servers = append(servers, *unityServer)
			}
			w.WriteHeader(http.StatusOK)
			json.NewEncoder(w).Encode(&unityServer)
		} else {
			w.WriteHeader(http.StatusPreconditionFailed)
		}
	} else {
		w.WriteHeader(http.StatusNoContent)
	}
}
