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

            instance.SetScale(_configuration.MaskScale());
            // public Vector2 MaskScale() =>
            //     return new Vector3(_fieldSize.x + 1, _fieldSize.y, 1)

            return instance;
        }
    }
}
