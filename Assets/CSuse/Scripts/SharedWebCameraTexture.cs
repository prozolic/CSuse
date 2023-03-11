using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SharedWebCameraTexture
{
    private static Dictionary<string, WebCamTexture> _webCamTextures = new Dictionary<string, WebCamTexture>();

    public static WebCamTexture CreateDefaultWebCamTexture()
    {
        var devices = WebCamTexture.devices;
        if (!devices.Any())
            throw new InvalidOperationException("Web Camera is not Connected.");
        return CreateDeviceWebCamTexture(devices[0].name, 1920, 1080, 30); // fps:30
    }

    public static WebCamTexture CreateDeviceWebCamTexture(string deviceName, int requestedWidth, int requestedHeight, int requestedFPS)
    {
        if (_webCamTextures.ContainsKey(deviceName))
        {
            return _webCamTextures[deviceName];
        }
        _webCamTextures.Add(deviceName, CreateWebCamTexture(deviceName, requestedWidth, requestedHeight, requestedFPS));
        return _webCamTextures[deviceName];
    }

    private static WebCamTexture CreateWebCamTexture(string deviceName, int requestedWidth, int requestedHeight, int requestedFPS) => new WebCamTexture(deviceName, 1920, 1080, 30);

}
