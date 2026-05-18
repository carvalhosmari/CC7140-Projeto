# CC7140-Projeto
Repositório destinado a alocação do projeto da disciplina CC7140 - Desenvolvimento de jogos digitais, ministrada no sétimo semestre do curso de Ciência da Computação do Centro Universitário FEI.

---

## Bem-vindo ao Frida's Adventures!

Controle **Frida**, uma gatinha aventureira, em **5 fases de plataforma** repletas de obstáculos, armadilhas e coletáveis. Avance pelo mapa, colete o máximo de itens e chegue ao fim de cada fase sem perder todas as suas vidas.

------

## Modos de Jogo

Antes de começar, escolha o modo no menu principal:

| Modo           | O que muda                                                   |
| -------------- | ------------------------------------------------------------ |
| **Modo Livre** | Sem limite de tempo — explore as fases no seu ritmo.         |
| **Modo Tempo** | Você tem **45 segundos** por fase. O contador fica **vermelho** quando restam 10 segundos. |

------

## Controles

| Ação       | Tecla                    |
| ---------- | ------------------------ |
| Mover      | `A` / `D` ou `←` / `→`   |
| Pular      | `Espaço` ou `↑`          |
| Pulo duplo | `Espaço` novamente no ar |
| Pausar     | `Esc`                    |

------

## Vidas e Game Over

- Você começa com **3 vidas**.
- Qualquer contato com um perigo consome uma vida e reinicia a fase.
- Ao perder as 3 vidas, aparece a tela de **Game Over**.
- No Game Over, você pode voltar ao menu principal e começar do zero.

------

## Pontuação

- Colete **peixes** para ganhar **10 pontos** cada.
- A pontuação acumula entre as fases - não zera ao avançar de nível.
- Se você morrer numa fase, a pontuação voltará ao que era quando **entrou nessa fase** (a de fases anteriores é preservada).
- O **Game Over** zera tudo.

------

## HUD (o que aparece na tela durante o jogo)

```
[⏱ 45s]          [❤ 3]          [🐟 0]
```

- **Esquerda** - tempo restante (só no Modo Tempo)
- **Centro** - vidas restantes
- **Direita** - sua pontuação atual

------

## Perigos - o que te mata

| Perigo             | Descrição                                                    |
| ------------------ | ------------------------------------------------------------ |
| **Espinho**        | Estático no chão: não pise.                                  |
| **Serra estática** | Serra parada em posição fixa: não encoste.                   |
| **Serra dinâmica** | Serra que se move de um lado para o outro: espere o momento certo para passar. |
| **Dogs**           | Inimigos que patrulham horizontalmente: desvie.              |
| **Fogo**           | Ao tocar, dá um breve sinal visual antes de causar dano: não encoste. |

------

## Mecânicas Especiais

### Plataformas que caem

Ao pousar sobre elas, você tem cerca de **0,5 segundo** antes de caírem. Pule logo!

### Trampolim

Ao pousar sobre ele, Frida é **lançada para cima** automaticamente - use para alcançar plataformas altas.

### Ventilador

Ao entrar na corrente de ar, o pulo fica **desativado** e Frida sobe pelo vento. Controle com precisão a posição horizontal com `A` / `D` para não cair nos espinhos ao redor.

### Caixa destrutível

Bata na caixa **5 vezes** para destruí-la e criar uma plataforma de acesso ou abrir uma passagem bloqueada. Cada batida empurra Frida de volta - tome cuidado com o espaço ao redor.

------

## As 5 Fases

| Fase       | Destaque                                                     |
| ---------- | ------------------------------------------------------------ |
| **Fase 1** | Introdução - percurso plano com muitos peixes. Aprenda os controles. |
| **Fase 2** | Plataformas que caem, espinhos e o primeiro Dog.             |
| **Fase 3** | Serras estáticas e em movimento, fogo e plataformas instáveis. |
| **Fase 4** | Caixa destrutível, trampolim e serras combinadas.            |
| **Fase 5** | Ventiladores e espinhos - fase de precisão no ar.            |

Ao completar a Fase 5, você chega à **tela final**. Parabéns!
