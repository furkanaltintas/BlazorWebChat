﻿using BlazorWebRtc_Application.Interface.Services.Manager;

namespace BlazorWebRtc_Application.Services.Manager;

public class ConnectionManager : IConnectionManager
{
    private static Dictionary<string, List<string>> _userConnections = new Dictionary<string, List<string>>();

    public void AddConnection(string userId, string connectionId)
    {
        lock (_userConnections)
            if (_userConnections.ContainsKey(userId)) _userConnections[userId].Add(connectionId);

            else _userConnections[userId] = new List<string> { connectionId };
    }

    public List<string> GetAllConnections() => _userConnections.Values.SelectMany(connections => connections).ToList();

    public List<string> GetAllUserIds() => _userConnections.Keys.ToList();

    public string GetConnection(string userId)
    {
        lock (_userConnections) return _userConnections.ContainsKey(userId) ? _userConnections[userId].FirstOrDefault()! : String.Empty;
    }

    public List<string> GetConnectionByUserId(List<string> userIds)
    {
        //var connections = new List<string>();
        //foreach (var userId in userIds)
        //    if (_userConnections.TryGetValue(userId, out var userConnections)) connections.AddRange(userConnections);
        //return connections;

        return userIds
            .Where(userId => _userConnections.TryGetValue(userId, out _))
            .SelectMany(userId => _userConnections[userId])
            .ToList();
    }

    public string GetConnectionByUserIdSingleObj(string userId)
    {
        if (_userConnections.TryGetValue(userId, out var userConnections) && userConnections.Any()) return userConnections.First();
        return null!;
    }

    public IEnumerable<string> GetConnections(string userId)
    {
        lock (_userConnections) return _userConnections.ContainsKey(userId) ? _userConnections[userId] : Enumerable.Empty<string>();
    }

    public List<string> GetSpecificConnections()
    {
        Dictionary<string, List<string>>.ValueCollection result = _userConnections.Values;
        return new List<string>();
    }

    public void RemoveConnection(string connectionId)
    {
        lock (_userConnections)
            foreach (var userId in _userConnections.Keys)
            {
                if (_userConnections[userId].Contains(connectionId)) _userConnections[userId].Remove(connectionId);
                if (!_userConnections[userId].Any()) _userConnections.Remove(userId);
                break;
            }
    }
}