# RinDotNet

汎用的な高速数値計算ライブラリを目指しています  
使用例
  
```csharp
            //  配列の生成
            float[] vec1 = Enumerable.Range(0, 10000).Select(x => (float)x).ToArray();
            float[] vec2 = Enumerable.Range(0, 10000).Select(x => (float)2 * x).ToArray();

            //Dot積を得る
            float dot = Vector.Dot(vec1, vec2);
            Console.WriteLine(dot);
```
ベクトルの内積を求める場合のベンチマークです
要素数が1万個の時のベンチマークです
|        Method |     Mean |     Error |    StdDev |   Median |
|-------------- |---------:|----------:|----------:|---------:|
|        RinDot | 1.382 us | 0.0557 us | 0.1641 us | 1.305 us |
| DotMathNetMKL | 1.986 us | 0.0925 us | 0.2655 us | 1.910 us |
  
そしてこれが要素数100万個の時のベンチマークです
|-------------- |---------:|---------:|---------:|
|        RinDot | 515.6 us | 10.15 us | 19.31 us |
| DotMathNetMKL | 514.5 us |  4.34 us |  4.06 us |

実装済み  
ベクトル内積
  
実装予定  
ベクトル演算  
行列演算  
機械学習  
  
ベクトル内積においてはi5 7200uのCPUにて60Gflops超え(要素数10000)を達成しており非常に高速です  
通常の配列を渡すだけで使えるお手軽使用なライブラリを製作していく予定です  
ぜひ支援等よろしくお願いします  
  
@rin_sns_  
https://www.amazon.co.jp/hz/wishlist/ls/IMC1G88FCO7X?ref_=wl_share
