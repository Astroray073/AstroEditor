using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MyStruct
{
    public int        _intField;
    public bool       _boolField;
    public Texture    _textureField;
    public GameObject _gameObjectField;
    public Vector2    _vector2Field;
    public Vector3    _vector3Field;
    public Renderer   _renderer;
    public Animator   _animator;
    public Collider   _collision;
}

public class SampleMono : MonoBehaviour
{
    public bool       _myBoolSwitch;
    public GameObject _myRef;
    public string     _myPath;
    public string     _myDirectory;

    public List<int>      _myIntList;
    public List<MyStruct> _myComplexList;
}