using UnityEngine;

namespace FunnySlots
{
    public class MaskFactory : IFactory<MaskView>
    {
        private readonly Configuration _configuration;

        public MaskFactory(Configuration configuration) => 
            _configuration = configuration;

        public MaskView Create()
        {
            MaskView instance = Object.Instantiate(_configuration.MaskView);
            
            instance.transform.localScale = new Vector3(
                _configuration.FieldSize.x + 1, 
                _configuration.FieldSize.y, 
                1);

            return instance;
        }
    }
}