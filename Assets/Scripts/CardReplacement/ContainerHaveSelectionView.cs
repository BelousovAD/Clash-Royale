using System.Collections.Generic;
using Item;
using Reflex.Attributes;
using UnityEngine;

namespace CardReplacement
{
    internal class ContainerHaveSelectionView : MonoBehaviour
    {
        [SerializeField] private ContainerType _containerType;
        [SerializeField] private List<GameObject> _haveSelectionObjects;
        [SerializeField] private List<GameObject> _haveNotSelectionObjects;
        
        private Container _container;

        [Inject]
        private void Initialize(IEnumerable<Container> containers)
        {
            foreach (Container container in containers)
            {
                if (container.Type == _containerType)
                {
                    _container = container;
                    break;
                }
            }
        }
        
        private void OnEnable()
        {
            _container.SelectChanged += UpdateView;
            UpdateView();
        }

        private void OnDisable() =>
            _container.SelectChanged -= UpdateView;

        private void UpdateView(Vector3 position)
        {
            _haveSelectionObjects.ForEach(gameObj => gameObj.SetActive(_container.Selected is not null));
            _haveNotSelectionObjects.ForEach(gameObj => gameObj.SetActive(_container.Selected is null));
        }       
        
        private void UpdateView()
        {
            _haveSelectionObjects.ForEach(gameObj => gameObj.SetActive(_container.Selected is not null));
            _haveNotSelectionObjects.ForEach(gameObj => gameObj.SetActive(_container.Selected is null));
        }
    }
}