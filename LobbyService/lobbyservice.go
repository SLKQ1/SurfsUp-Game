package main

import (
	"fmt"
	"net/http"

	"github.com/gorilla/mux"
)

func lobbyService(w http.ResponseWriter, r *http.Request) {
	fmt.Fprintln(w, "Surfs Up! - Lobby Service")
	fmt.Fprintf(w, "Current Version: 1.0.0")
}

func main() {
	router := mux.NewRouter().StrictSlash(true)
	router.HandleFunc("/", lobbyService)
	http.ListenAndServe(":8080", router)

}
