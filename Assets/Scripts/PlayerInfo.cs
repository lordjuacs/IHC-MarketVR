using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}

public class PlayerInfo : MonoBehaviour
{
    private Dictionary<string, PlayerData[]> playerCache = new Dictionary<string, PlayerData[]>();
    private Dictionary<string, string> playerPosition = new Dictionary<string, string>
    {
        { "Forwards", "Delantero" },
        { "Goalkeepers", "Arquero" },
        { "Defenders", "Defensor" },
        { "Midfielders", "Mediocampista" }

    };
    string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }

    // Método para realizar la solicitud a la API y procesar la respuesta
    IEnumerator GetPlayerInfo(string playerId)
    {
        // Check if player data is already cached
        if (playerCache.ContainsKey(playerId))
        {
            Debug.Log("I USED CACHE" + playerId);
            // Use cached data
            UpdateUI(playerId, playerCache[playerId]);
            yield break;
        }
        string APIkey = "6f4a17bf86648f57c3ebd1f44577a288820717c1127ed6bc49f49fd834e12f73";
        string apiUrl = "https://apiv3.apifootball.com/?action=get_players&player_id=" + playerId + "&APIkey=" + APIkey;

        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl))
        {

            Debug.Log("I DID NOT USE CACHE" + playerId);

            // Inicia la solicitud y espera la respuesta
            yield return webRequest.SendWebRequest();

            // Verifica si hay algún error
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                GameObject nameText = GameObject.Find("name_" + playerId);
                nameText.GetComponent<TextMeshProUGUI>().text = "ERROR";
                Debug.LogError("Error en la solicitud: " + webRequest.error);
            }
            else
            {
                string jsonResponse = webRequest.downloadHandler.text;
                string jsonString = fixJson(jsonResponse);

                Debug.Log(jsonString);
                PlayerData[] player = JsonHelper.FromJson<PlayerData>(jsonString);

                Debug.Log("Player name: " + player[0].player_name);

                // Cache the player data
                playerCache[playerId] = player;

                // Update the UI with the retrieved data
                UpdateUI(playerId, player);
            }
        }
    }

    private void UpdateUI(string playerId, PlayerData[] player)
    {
        GameObject nameText = GameObject.Find("name_" + playerId);
        GameObject longnameText = GameObject.Find("longname_" + playerId);
        GameObject statsText = GameObject.Find("stats_" + playerId);
        GameObject infoText = GameObject.Find("info_" + playerId);
        GameObject extraText = GameObject.Find("extra_" + playerId);

        if (nameText != null)
        {
            int pos = 0;
            string country = "";
            if (playerId == "2354545782")
            {
                pos = 1;
                country = "Francia";
            }
            else if (playerId == "1135663375")
            {
                country = "Argentina";
            }
            else if (playerId == "322456720")
            {
                country = "Brasil";
            }
            else if (playerId == "103051168")
            {
                country = "Portugal";
            }
            Debug.Log(playerId);
            Debug.Log(pos);
            Debug.Log(player[pos].player_name);

            nameText.GetComponent<TextMeshProUGUI>().text = player[pos].player_name;
            longnameText.GetComponent<TextMeshProUGUI>().text = player[pos].player_complete_name;

            statsText.GetComponent<TextMeshProUGUI>().text = "Goles: " + player[pos].player_goals +
                                                            "\nAsist.: " + player[pos].player_assists +
                                                            "\nTiros: " + player[pos].player_shots_total;

            infoText.GetComponent<TextMeshProUGUI>().text = "Edad: " + player[pos].player_age +
                                                            "\nPaís: " + country +
                                                            "\nEquipo: " + player[pos].team_name;

            extraText.GetComponent<TextMeshProUGUI>().text = "Posición: " + playerPosition[player[pos].player_type] +
                                                            "\nDorsal: " + player[pos].player_number +
                                                            "\nCalificación: " + player[pos].player_rating;
        }
    }

    public void CallApi(string playerId)
    {
        StartCoroutine(GetPlayerInfo(playerId));
    }

    // Clase para almacenar los datos del jugador si deseas convertir el JSON a un objeto C#
    [System.Serializable]
    public class PlayerData
    {
        public int player_key;
        public string player_id;
        public string player_image;
        public string player_name;
        public string player_complete_name;
        public string player_number;
        public string player_country;
        public string player_type;
        public string player_age;
        public string player_birthdate;
        public string player_match_played;
        public string player_goals;
        public string player_yellow_cards;
        public string player_red_cards;
        public string player_minutes;
        public string player_injured;
        public string player_substitute_out;
        public string player_substitutes_on_bench;
        public string player_assists;
        public string player_is_captain;
        public string player_shots_total;
        public string player_goals_conceded;
        public string player_fouls_committed;
        public string player_tackles;
        public string player_blocks;
        public string player_crosses_total;
        public string player_interceptions;
        public string player_clearances;
        public string player_dispossesed;
        public string player_saves;
        public string player_inside_box_saves;
        public string player_duels_total;
        public string player_duels_won;
        public string player_dribble_attempts;
        public string player_dribble_succ;
        public string player_pen_comm;
        public string player_pen_won;
        public string player_pen_scored;
        public string player_pen_missed;
        public string player_passes;
        public string player_passes_accuracy;
        public string player_key_passes;
        public string player_woordworks;
        public string player_rating;
        public string team_name;
        public string team_key;
    }
}
