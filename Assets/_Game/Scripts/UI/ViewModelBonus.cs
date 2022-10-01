using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;

[Binding]
public class ViewModelBonus : MonoBehaviour, INotifyPropertyChanged
{
    [SerializeField] private Image icon;

    private string nameBonus = " ";
    private string costBonus = "0";
    private Sprite iconBonus;
    
    public event PropertyChangedEventHandler PropertyChanged;

    [Binding]
    public string NameBonus
    { 
        get => nameBonus;
        set 
        {
            if (nameBonus.Equals(value)) return;

            nameBonus = value.ToUpper();
            OnPropertyChanged("NameBonus");
        }
    }

    [Binding]
    public string CostBonus
    {
        get => costBonus;
        set
        {
            if (costBonus.Equals(value)) return;

            costBonus = value;
            OnPropertyChanged("CostBonus");
        }
    }

    public void SetIconBonus(Sprite _icon)
    {
        icon.sprite = _icon;
    }

    private void OnPropertyChanged(string propertyName)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
