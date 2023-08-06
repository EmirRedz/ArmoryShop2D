using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    public const string COIN_AMOUNT_KEY = "CoinAmount";
    
    [SerializeField] private TMP_Text coinAmountText;
    private int coinAmount;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        coinAmount = PlayerPrefs.GetInt(COIN_AMOUNT_KEY, 100);
        coinAmountText.SetText(coinAmount.ToString());
    }

    public void AddCoin(int amount)
    {
        coinAmount += amount;
        SaveCoinAmount();
    }
    

    public void RemoveCoin(int amount)
    {
        coinAmount -= amount;
        if (coinAmount <= 0)
        {
            coinAmount = 0;
        }
        SaveCoinAmount();
    }

    public int CurrentCoinAmount()
    {
        return coinAmount;
    }
    private void SaveCoinAmount()
    {
        PlayerPrefs.SetInt(COIN_AMOUNT_KEY, coinAmount);
        PlayerPrefs.Save();
        coinAmountText.SetText(coinAmount.ToString());
        
        ShopManager.Instance.CheckIfCanBuy();
    }
}
