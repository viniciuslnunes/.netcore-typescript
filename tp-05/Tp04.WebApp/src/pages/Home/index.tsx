import React, { useCallback, useEffect, useState } from "react";

import getSalutation from "../../utils/getSalutation";

import * as SC from "./styles";
import apiConfig from "../../config/api";
import axios from "axios";
import { BiEdit, BiError, BiPlus, BiX } from "react-icons/bi";
import { Link, useHistory, useLocation, useRouteMatch } from "react-router-dom";
import Button from "../../components/Button";
import IBook from "../../IBook";
import { routesMap } from "../../config/routes-map";

export interface IProduct {
  id: number | null;
  idUsuarioUpdate?: number;
  usuarioUpdate?: IBook;
  idUsuarioCadastro?: number;
  usuarioCadastro?: IBook;
  nome: string;
  preco: number;
  status: boolean;
}

const Home: React.FC = () => {
  const [books, setBooks] = useState<IProduct[]>([]);
  const history = useHistory();

  const salutation = getSalutation();

  const loadBooks = useCallback(() => {
    axios
      .get<IProduct[]>(`${apiConfig.baseUrl}/produtos`, {})
      .then((response) => setBooks(response.data))
      .catch((err) => console.log(err));
  }, []);

  useEffect(() => {
    const token = localStorage.getItem("token");

    if (!token) {
      history.push(routesMap.Login.path);
    } else {
      axios
        .get<IBook[]>(`${apiConfig.baseUrl}/sessions/check`, {
          headers: { Authorization: `Bearer ${token}` },
        })
        .then(() => loadBooks())
        .catch((err) => history.push(routesMap.Login.path));
    }
  }, []);

  const handleDelete = useCallback((bookId: number) => {
    // eslint-disable-next-line no-restricted-globals
    if (confirm("Vai jogar fora memo, irmão?")) {
      axios
        .delete<IBook[]>(`${apiConfig.baseUrl}/produtos/${bookId}`, {})
        .then(() => loadBooks())
        .catch((err) => console.log(err));
    }
  }, []);

  const handleEdit = useCallback((bookId: number) => {
    history.push(`${routesMap.Settings.path}?userId=${bookId}`);
  }, []);

  return (
    <SC.Container>
      <SC.Title>{salutation}</SC.Title>

      <Link className="button" to={routesMap.Settings.path}>
        Adicionar produto
      </Link>

      <SC.MainContent>
        {books.length ? (
          <SC.BookList>
            <thead>
              <tr>
                <th>Produto</th>
                <th>Preço</th>
                <th>Criado por</th>
                <th>Atualizado por</th>

                <td className="icon">Apagar</td>
                <td className="icon">Editar</td>
              </tr>
            </thead>
            <tbody>
              {books.map((book) => (
                <tr key={Number(book.id)}>
                  <td>{book.nome}</td>
                  <td>{book.preco}</td>
                  <td>{book.usuarioCadastro?.nome}</td>
                  <td>{book.usuarioUpdate?.nome}</td>

                  <td className="icon">
                    <Button>
                      <BiEdit
                        color="#FBBC05"
                        size={24}
                        onClick={() => handleEdit(Number(book.id))}
                      />
                    </Button>
                  </td>
                  <td className="icon">
                    <Button>
                      <BiX
                        color="#e85b51"
                        size={24}
                        onClick={() => handleDelete(Number(book.id))}
                      />
                    </Button>
                  </td>
                </tr>
              ))}
            </tbody>
          </SC.BookList>
        ) : (
          <strong>Tem produto não, maluko. Sai daqui</strong>
        )}
      </SC.MainContent>
    </SC.Container>
  );
};

export default Home;
