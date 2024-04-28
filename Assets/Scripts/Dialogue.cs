using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue
{
    private string _nameOfCharacter;
    public string NameOfCharacter => _nameOfCharacter;
    private string _message;
    public string Message => _message;
    private string[] _response;
    public string[] Response => _response;
    private int[] _targetForResponse;
    public int[] TargetForResponse => _targetForResponse;
    public Dialogue(string nameOfCharacter, string message, string[] response, int[] targetForResponse)
    {
        _nameOfCharacter = nameOfCharacter;
        _message = message;
        _response = response;
        _targetForResponse = targetForResponse;
    }
}
