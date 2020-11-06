# React template

### Struktura komponentu

#### Wzór
```
// Thrid party imports

// Routes

// Local imports

// Additional styling

// Component
```

#### Przykład
```
// Thrid party imports
import React, { MouseEvent, useState } from './node_modules/react';
import {Button} from "./node_modules/antd";

// Routes
import Login from "/src/App/routes/Login";

// Local imports
import {Component} from "/src/App/components/shared/Component";

// Additional styling
import './App.css';

// Component
const App: React.FC = () =>  {

  return (
    <div className="App">
    </div>
  );
}

export default App;
```
