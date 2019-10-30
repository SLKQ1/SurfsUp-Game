package serverutils

import (
	"encoding/json"
	"io/ioutil"
	"os"
)

const ConfigFile = "config.json"

type Config struct {
	Timeout        int    `json:"timeout"`        // Timeout in seconds to automatically terminate a server after
	ExecutablePath string `json:"executablePath"` // The path to the headless server executable
}

// ReadConfig() will read the configuration file and return the appropraite
// Config struct with the values unmarshalled.
func ReadConfig() Config {
	var config Config
	configFile, _ := os.Open(ConfigFile)
	readByte, _ := ioutil.ReadAll(configFile)
	json.Unmarshal(readByte, &config)
	return config
}
