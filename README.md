# RinDotNet

�ėp�I�ȍ������l�v�Z���C�u������ڎw���Ă��܂�  
�g�p��
  
```
            //  �z��̐���
            float[] vec1 = Enumerable.Range(0, 10000).Select(x => (float)x).ToArray();
            float[] vec2 = Enumerable.Range(0, 10000).Select(x => (float)2 * x).ToArray();

            //Dot�ς𓾂�
            float dot = Vector.Dot(vec1, vec2);
            Console.WriteLine(dot);
```



�����ς�  
�x�N�g������
  
�����\��  
�x�N�g�����Z  
�s�񉉎Z  
�@�B�w�K  
  
�x�N�g�����ςɂ����Ă�i5 7200u��CPU�ɂ�60Gflops����(�v�f��10000)��B�����Ă�����ɍ����ł�  
�ʏ�̔z���n�������Ŏg���邨��y�g�p�ȃ��C�u�����𐻍삵�Ă����\��ł�  
���Ўx������낵�����肢���܂�  
  
@rin_sns_  
https://www.amazon.co.jp/hz/wishlist/ls/IMC1G88FCO7X?ref_=wl_share