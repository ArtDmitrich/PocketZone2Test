using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public float CooldownTime => _cooldownTime;
    public int MinDamage => _minDamage;
    public int MaxDamage => _maxDamage;
    
    [SerializeField] private float _cooldownTime;
    [SerializeField] private int _minDamage;
    [SerializeField] private int _maxDamage;
}
