using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using Zenject;

public class InAppManager : MonoBehaviour, IStoreListener
{
    [SerializeField] private PurchaseOneTimeOffer _purchaseOneTimeOffer;

    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;

    public const string pJunkBotSet = "junk_bot_set";
    public const string pSpecialOffer = "special_offer";
    public const string pSmallCrystals = "small_crystals";
    public const string pMiddleCrystals = "middle_crystals";
    public const string pBigCrystals = "big_crystals";
    public const string pBonusCrusher = "bonus_crusher";
    public const string pBonusForce = "bonus_force";
    public const string pBonusTrailer = "bonus_trailer";
    public const string pBonusMagnet = "bonus_magnet";
    public const string pNoAds = "no_ads";
    public const string pOneTimeOffer = "one_time_offer";

    #region Injects

    private IAPController _iapController;    

    [Inject]
    private void Construct(IAPController iapController)
    {
        _iapController = iapController;
    }

    #endregion

    void Start()
    {
        if (m_StoreController == null)
        {
            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {
        if (IsInitialized())
        {
            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(pJunkBotSet, ProductType.NonConsumable);
        builder.AddProduct(pSpecialOffer, ProductType.NonConsumable);
        builder.AddProduct(pBonusCrusher, ProductType.NonConsumable);
        builder.AddProduct(pBonusForce, ProductType.NonConsumable);
        builder.AddProduct(pBonusTrailer, ProductType.NonConsumable);
        builder.AddProduct(pBonusMagnet, ProductType.NonConsumable);
        builder.AddProduct(pNoAds, ProductType.NonConsumable);
        builder.AddProduct(pOneTimeOffer, ProductType.NonConsumable);

        builder.AddProduct(pSmallCrystals, ProductType.Consumable);
        builder.AddProduct(pMiddleCrystals, ProductType.Consumable);
        builder.AddProduct(pBigCrystals, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void BuyProductID(string productId)
    {
        try
        {
            if (IsInitialized())
            {
                Product product = m_StoreController.products.WithID(productId);

                if (product != null && product.availableToPurchase)
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));// ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
                    m_StoreController.InitiatePurchase(product);
                }
                else
                {
                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                }
            }
            else
            {
                Debug.Log("BuyProductID FAIL. Not initialized.");
            }
        }
        catch (Exception e)
        {
            Debug.Log("BuyProductID: FAIL. Exception during purchase. " + e);
        }
    }

    public void RestorePurchases()
    {
        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("RestorePurchases started ...");

            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            apple.RestoreTransactions((result) =>
            {
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        else
        {
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }
     
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: Completed!");

        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
        
        if (String.Equals(args.purchasedProduct.definition.id, pJunkBotSet, StringComparison.Ordinal))
        {
            _iapController.OnPurchaseJunkBotSetCompleted(pJunkBotSet, args.purchasedProduct.transactionID);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, pSpecialOffer, StringComparison.Ordinal))
        {
            _iapController.OnPurchaseSpecialOfferCompleted(pSpecialOffer, args.purchasedProduct.transactionID);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, pSmallCrystals, StringComparison.Ordinal))
        {
            _iapController.OnPurchaseSmallHard(pSmallCrystals, args.purchasedProduct.transactionID);
        }
        else if(String.Equals(args.purchasedProduct.definition.id, pMiddleCrystals, StringComparison.Ordinal))
        {
            _iapController.OnPurchaseMiddleHard(pMiddleCrystals, args.purchasedProduct.transactionID);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, pBigCrystals, StringComparison.Ordinal))
        {
            _iapController.OnPurchaseBigHard(pBigCrystals, args.purchasedProduct.transactionID);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, pBonusCrusher, StringComparison.Ordinal))
        {
            _iapController.OnPurchaseCrusherBoost(pBonusCrusher, args.purchasedProduct.transactionID);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, pBonusForce, StringComparison.Ordinal))
        {
            _iapController.OnPurchaseForceBoost(pBonusForce, args.purchasedProduct.transactionID);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, pBonusTrailer, StringComparison.Ordinal))
        {
            _iapController.OnPurchaseTrailerBoost(pBonusTrailer, args.purchasedProduct.transactionID);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, pBonusMagnet, StringComparison.Ordinal))
        {
            _iapController.OnPurchaseMagnetBoost(pBonusMagnet, args.purchasedProduct.transactionID);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, pNoAds, StringComparison.Ordinal))
        {
            _iapController.OnPurchaseTruNoAds(pNoAds, args.purchasedProduct.transactionID);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, pOneTimeOffer, StringComparison.Ordinal))
        {
            _purchaseOneTimeOffer.OnPurchaseOneTimeOfferCompleted(pOneTimeOffer, args.purchasedProduct.transactionID);
        }

        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}
