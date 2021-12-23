import React, { useCallback, useEffect, useState } from 'react'

import getSalutation from '../../utils/getSalutation'

import * as SC from './styles'
import IBook from '../CheckOwner/interfaces/IBook'
import Input from '../../components/Input'
import apiConfig from '../../config/api'
import axios from 'axios'
import { BiEdit, BiError, BiPlus, BiX } from 'react-icons/bi'
import { Link, useHistory, useLocation, useRouteMatch } from 'react-router-dom'
import { routesMap } from '../../config/routes-map'
import Button from '../../components/Button'

const Home: React.FC = () => {
  const [books, setBooks] = useState<IBook[]>([])
  const history = useHistory()

  const salutation = getSalutation()

  const loadBooks = useCallback(() => {
    axios
      .get<IBook[]>(`${apiConfig.baseUrl}/usuarios`, {})
      .then((response) => setBooks(response.data))
      .catch((err) => console.log(err))
  }, [])

  useEffect(() => loadBooks(), [])

  const handleDelete = useCallback((bookId: number) => {
    if (confirm('Vai jogar fora memo, irmão?')) {
      axios
        .delete<IBook[]>(`${apiConfig.baseUrl}/usuarios/${bookId}`, {})
        .then(() => loadBooks())
        .catch((err) => console.log(err))
    }
  }, [])

  const handleEdit = useCallback((bookId: number) => {
    history.push(`${routesMap.Settings.path}?userId=${bookId}`)
  }, [])

  return (
    <SC.Container>
      <SC.Title>{salutation}</SC.Title>

      <Link className="button" to={routesMap.Settings.path}>
        Adicionar usuário
      </Link>

      <SC.MainContent>
        {books.length ? (
          <SC.BookList>
            <thead>
              <tr>
                <th>Login</th>
                <th>Senha</th>

                <td className="icon">Apagar</td>
                <td className="icon">Editar</td>
              </tr>
            </thead>
            <tbody>
              {books.map((book) => (
                <tr key={Number(book.id)}>
                  <td>{book.nome}</td>
                  <td>{book.senha}</td>

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
          <strong>Tem usuário não, maluko. Sai daqui</strong>
        )}
      </SC.MainContent>
    </SC.Container>
  )
}

export default Home
