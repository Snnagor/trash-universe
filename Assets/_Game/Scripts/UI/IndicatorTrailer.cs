using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TrailerTrash))]
public class IndicatorTrailer : MonoBehaviour
{
    [SerializeField] private Color _emptyColor;
    [SerializeField] private Color _middleColor;
    [SerializeField] private Color _preFullColor;
    [SerializeField] private Color _fullColor;

    private TrailerTrash trailerTrash;

    #region Injects
    
    private ViewModel _viewModel;

    [Inject]
    private void Construct(ViewModel viewModel)
    {        
        _viewModel = viewModel;
    }
    #endregion

    private void Awake()
    {
        trailerTrash = GetComponent<TrailerTrash>();
    }

    void Start()
    {        
        _viewModel.ColorFillImage = _emptyColor;
        UpdateUIFullTrailer();
    }

    public void UpdateUIFullTrailer()
    {       
        _viewModel.ItemInTrailer = trailerTrash.ItemInTrailer.Count + "/" + ((trailerTrash.CountMaxItemInTrailer > 10000)? "Ꝏ" : trailerTrash.CountMaxItemInTrailer.ToString());

        float percentFull = (float)trailerTrash.ItemInTrailer.Count / trailerTrash.CountMaxItemInTrailer;

        if (percentFull == 1f )
        {
            _viewModel.ColorFillImage = _fullColor;
        }
        else if (percentFull > 0.85f)
        {
            _viewModel.ColorFillImage = _preFullColor;
        }
        else if (percentFull > 0.6f )
        {
            _viewModel.ColorFillImage = _middleColor;
        }
        else if (percentFull < 0.6f )
        {           
            _viewModel.ColorFillImage = _emptyColor;            
        }

        _viewModel.FillImage = percentFull;
    }

}
